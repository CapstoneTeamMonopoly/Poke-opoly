using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static int currPlayer;

    // Objects that the GameManager controls
    private static GameObject board;
    private static List<GameObject> players;
    private static List<GameObject> tiles;
    private static List<GameObject> dice;
    // Community chest and other deck should be here

    // Used to see if turn changes or if the same player goes twice
    public static bool doubles { get; set; }

    // EventHandler used to create coroutines
    private static EventHandler handler;

    static GameManager()
    {
        currPlayer = -1;
        doubles = false;
        state = GameState.RollDice;
    }

    public enum GameState
    {
        RollDice,
        BuyProperty,
        UpgradeProperty,
    }

    private static GameState state;

    public static void InitGameObjects(GameObject board, ref List<GameObject> players, ref List<GameObject> tiles, ref List<GameObject> dice)
    {
        GameManager.players = players;
        GameManager.tiles = tiles;
        GameManager.dice = dice;
        GameManager.board = board;
        handler = board.AddComponent<EventHandler>();
        ChangeState(state);
    }
    
    // Used to internally change state when changing selectable components is necessary
    private static void ChangeState(GameState toState)
    {
        foreach (GameObject tile in tiles) {
            tile.GetComponent<BasicTile>().CanSelect = false;
        }
        foreach (GameObject die in dice)
        {
            die.GetComponent<Dice>().actionable = false;
        }

        switch (toState)
        {
            case GameState.RollDice:
                foreach (GameObject die in dice)
                {
                    die.GetComponent<Dice>().actionable = true;
                }
                if (!doubles)
                {
                    IncrementTurn();
                }
                doubles = false;
                break;
            case GameState.BuyProperty:
            case GameState.UpgradeProperty:
                // Allow skip button to be selectable, fall through for all states that do this
                // TODO: Create a skip button
                break;
        }
        state = toState;
    }

    // Used for incrementing turn, recursion to skip bankrupted players
    private static void IncrementTurn()
    {
        currPlayer += 1;
        if (currPlayer >= 4)
        {
            currPlayer = 0;
        }
        if (players[currPlayer].GetComponent<Player>().bankrupt)
        {
            IncrementTurn();
        }
    }

    public static void RollDice()
    {
        foreach (GameObject die in dice)
        {
            die.GetComponent<Dice>().actionable = false;
        }
        board.GetComponent<Board>().StartCoroutine(handler.StartRollEvent(dice[0], dice[1]));
    }

    // Called when the player moves based on rolling a dice, DON'T CALL VIA RAILROAD MOVING
    public static void MovePlayerByDice(int dist)
    {
        // Get player and move
        Player player = players[currPlayer].GetComponent<Player>();
        player.position += dist;
        if (player.position >= 40)
        {
            player.money += 200;
        }
        player.position %= 40;
        player.MovePlayer(tiles[player.position]);

        // Call functionality for landing on a tile
        tiles[player.position].GetComponent<BasicTile>().OnLand();
    }

    public static void TileClicked(int index)
    {
        // Depending on the state, the function of tile clicked changes
        switch (state)
        {
            case GameState.BuyProperty:
                // Set property owner to current player, subtract player money, change state to roll dice
                PropertyTile boughtTile = tiles[index].GetComponent<PropertyTile>();
                Debug.Log($"Bought tile {index}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - boughtTile.PurchasePrice}"); // TODO: Delete once hands implemented
                boughtTile.Owner = currPlayer;
                boughtTile.Level += 1;
                players[currPlayer].GetComponent<Player>().money -= boughtTile.PurchasePrice;
                ChangeState(GameState.RollDice);
                break;
            case GameState.UpgradeProperty:
                PropertyTile upgradedTile = tiles[index].GetComponent<PropertyTile>();
                Debug.Log($"Upgraded tile {index}, level: {upgradedTile.Level + 1}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - upgradedTile.PurchasePrice}"); // TODO: Delete once hands implemented
                upgradedTile.Level += 1;
                players[currPlayer].GetComponent<Player>().money -= upgradedTile.PurchasePrice;
                ChangeState(GameState.RollDice);
                break;
        }
    }

    public static void BuyPropertyRoutine(int index)
    {
        ChangeState(GameState.BuyProperty);
        // TODO: check if the tile costs more money than the player has and only allow selection if it doesn't
        tiles[index].GetComponent<PropertyTile>().CanSelect = true;
    }

    public static void EndTileRoutine()
    {
        ChangeState(GameState.RollDice);
    }

    public static void PayOnLand(int propertyIndex)
    {
        PropertyTile tile = tiles[propertyIndex].GetComponent<PropertyTile>();
        Debug.Log($"Player {currPlayer} landed on property owned by player {tile.Owner}");
        if (tile.Owner != currPlayer)
        {
            int cost = tile.BaseLandingPrice * tile.Level;
            if (tile.FullSet)
            {
                cost *= 2;
            }
            players[currPlayer].GetComponent<Player>().money -= cost;
            players[tile.Owner].GetComponent<Player>().money += cost;
            if (players[currPlayer].GetComponent<Player>().money < 0)
            {
                BankruptCurrentPlayer(tile.Owner);
            }
            GameManager.EndTileRoutine();
        }
        else
        {
            if (tile.Level < 3)
            {
                Debug.Log("Upgrade property state");
                ChangeState(GameState.UpgradeProperty);
                // TODO: check if the tile upgrade costs more money than the player has and only allow selection if it doesn't
                tiles[propertyIndex].GetComponent<PropertyTile>().CanSelect = true;
            }
            else
            {
                GameManager.EndTileRoutine();
            }
        }
    }

    private static void BankruptCurrentPlayer(int debtedPlayer)
    {
        Player bankruptPlayer = players[currPlayer].GetComponent<Player>();
        bankruptPlayer.bankrupt = true;
        bankruptPlayer.money = 0;
        foreach (GameObject tile in tiles)
        {
            PropertyTile property = tile.GetComponent<PropertyTile>();
            if (property != null) // If tile is actually a property
            {
                if (property.Owner == currPlayer)
                {
                    property.Owner = debtedPlayer;
                    Debug.Log($"Transfered property ${property.index} from player {currPlayer} to {debtedPlayer}");
                }
            }
        }
        players[currPlayer].GetComponent<Player>().DestroyPlayer();
        // TODO: check if only 1 player is left and finish the game if so
    }
}
