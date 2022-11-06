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
        PlayerAction,
        DrawCard,
        BuyProperty,
        UpgradeProperty,
        Railroad,
        PokemonCenter,
        TeamRocket,
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
                if (boughtTile.Level == 0) boughtTile.Level = 1;
                // TODO: See if the player should be charged when picking up tiles previously bought by other players (case: Level != 0, if they shouldnt be charged, add line to the if statement above)
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
            case GameState.Railroad:
                Player player = players[currPlayer].GetComponent<Player>();

                int start = players[currPlayer].GetComponent<Player>().position;

                int price = index - start;
                if (price < 0) price += 40;
                price *= 5;

                Debug.Log($"Selected railroad tile {index}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - price}"); // TODO: Delete once hands implemented
                player.money -= price;
                player.MovePlayer(tiles[index]);
                player.position = index;

                ChangeState(GameState.RollDice);
                break;
            case GameState.PokemonCenter:
                PropertyTile freeUpgradedTile = tiles[index].GetComponent<PropertyTile>();
                Debug.Log($"Upgraded tile {index} for free. Property goes from level {freeUpgradedTile.Level} to player {freeUpgradedTile.Level + 1}"); // TODO: Delete once hands implemented

                freeUpgradedTile.Level += 1;

                ChangeState(GameState.RollDice);
                break;
            case GameState.TeamRocket:
                PropertyTile stolenTile = tiles[index].GetComponent<PropertyTile>();
                Debug.Log($"Stole tile {index}. Property goes from player {stolenTile.Owner} to player {currPlayer}"); // TODO: Delete once hands implemented

                stolenTile.Owner = currPlayer;

                ChangeState(GameState.RollDice);
                break;
        }
    }

    /*
     *  ROUTINE FUNCTIONS
     *  
     *  All routine functions must change state at the end of all execution paths
     *  
     *  Note: changing state removes all tile selectability so let tiles be selectable after changing state
     */

    public static void EndTileRoutine()
    {
        ChangeState(GameState.RollDice);
    }

    public static void BuyPropertyRoutine(int index)
    {
        ChangeState(GameState.BuyProperty);

        // Check if the tile costs more money than the player has and only allow selection if it doesn't
        PropertyTile tile = tiles[index].GetComponent<PropertyTile>();
        if (players[currPlayer].GetComponent<Player>().money >= tile.PurchasePrice)
        {
            tiles[index].GetComponent<PropertyTile>().CanSelect = true;
        }
    }

    public static void PayOnLandRoutine(int propertyIndex)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        PropertyTile tile = tiles[propertyIndex].GetComponent<PropertyTile>();
        Debug.Log($"Player {currPlayer} landed on property owned by player {tile.Owner}"); // TODO: Delete once hands implemented
        if (tile.Owner != currPlayer)
        {
            int cost = tile.BaseLandingPrice * tile.Level;
            if (tile.FullSet)
            {
                cost *= 2;
            }
            player.money -= cost;
            player.money += cost;
            if (player.money < 0)
            {
                BankruptCurrentPlayer(tile.Owner);
            }
            ChangeState(GameState.RollDice);
        }
        else
        {
            if (tile.Level < 3)
            {
                ChangeState(GameState.UpgradeProperty);

                // Check if the tile upgrade costs more money than the player has and only allow selection if it doesn't
                if (player.money >= tile.PurchasePrice)
                {
                    tile.CanSelect = true;
                }
            }
            else
            {
                ChangeState(GameState.RollDice);
            }
        }
    }

    public static void RailroadRoutine(int railroadIndex)
    {
        ChangeState(GameState.Railroad);

        foreach (GameObject tile in tiles)
        {
            RailroadTile railTile = tile.GetComponent<RailroadTile>();
            if (railTile != null) 
            {
                // Checks if the player can afford to travel to a railroad tile
                Player player = players[currPlayer].GetComponent<Player>();

                int start = players[currPlayer].GetComponent<Player>().position;

                int price = railTile.index - start;
                if (price < 0) price += 40;
                price *= 5;

                if (railTile.index != railroadIndex && price <= player.money) railTile.CanSelect = true;
            }
        }
    }

    public static void TeamRocketRoutine()
    {
        ChangeState(GameState.TeamRocket);

        foreach (GameObject tile in tiles)
        {
            PropertyTile property = tile.GetComponent<PropertyTile>();
            if (property != null)
            {
                // If the tile is owned but not by the current player, it is eligible to steal
                if (property.Owner != currPlayer && property.Owner != -1)
                {
                    property.CanSelect = true;
                }
            }
        }
    }

    public static void PokemonCenterRoutine()
    {
        ChangeState(GameState.PokemonCenter);

        foreach (GameObject tile in tiles)
        {
            PropertyTile property = tile.GetComponent<PropertyTile>();
            if (property != null)
            {
                // If the tile is owned by the current player and not max level, it is eligible for upgrade
                if (property.Owner == currPlayer && property.Level < 3)
                {
                    property.CanSelect = true;
                }
            }
        }
    }

    public static void TaxRoutine(int tax)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        player.money -= tax;
        if (player.money < 0)
        {
            // Player lost to taxes so their pokemon are released back to have no owner
            BankruptCurrentPlayer(-1);
        }

        ChangeState(GameState.RollDice);
    }

    /*
     * DICE FUNCTIONS
     */

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

    /*
     * PRIVATE HELPER FUNCTIONS
     */

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

    // Used to internally change state when changing selectable components is necessary
    private static void ChangeState(GameState toState)
    {
        foreach (GameObject tile in tiles)
        {
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
            // These states all require the skip button to be selectable, uses case fallthrough
            case GameState.PokemonCenter:
            case GameState.TeamRocket:
            case GameState.BuyProperty:
            case GameState.UpgradeProperty:
            case GameState.Railroad:
                // Allow skip button to be selectable
                // TODO: Create a skip button
                break;
        }
        state = toState;
    }
}