using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameManager
{
    private static int currPlayer;

    // Objects that the GameManager controls
    private static GameObject skipButton;
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
        currPlayer = 0;
        doubles = false;
        state = GameState.RollDice;
    }

    public enum GameState
    {
        RollDice,
        PlayerAction,
        DrawCard,
        BuyProperty,
        BuyUtility,
        UpgradeProperty,
        Railroad,
        PokemonCenter,
        TeamRocket,
        GameEnd,
    }

    private static GameState state;

    public static void InitGameObjects(GameObject board, ref List<GameObject> players, ref List<GameObject> tiles, ref List<GameObject> dice)
    {
        GameManager.players = players;
        GameManager.tiles = tiles;
        GameManager.dice = dice;
        GameManager.board = board;
        GameManager.skipButton = new GameObject("skip", typeof(BoxCollider), typeof(SpriteRenderer), typeof(SkipButton));
        skipButton.transform.position = new Vector3(9, -5, 0);
        skipButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/skip");
        skipButton.GetComponent<SpriteRenderer>().enabled = false;
        handler = board.AddComponent<EventHandler>();
    }

    /*
     *  INTERACTIVITY
     */

    public static void TileClicked(int index)
    {
        // Depending on the state, the function of tile clicked changes
        switch (state)
        {
            case GameState.BuyProperty:
                // Set property owner to current player, subtract player money, change state to roll dice
                PropertyTile boughtTile = tiles[index].GetComponent<PropertyTile>();

                GivePlayerProperty(currPlayer, index);
                Debug.Log($"Bought tile {index}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - (boughtTile.PurchasePrice * boughtTile.Level)}"); // TODO: Delete once hands implemented
                players[currPlayer].GetComponent<Player>().money -= boughtTile.PurchasePrice * boughtTile.Level;

                ChangeState(GameState.RollDice);
                break;
            case GameState.BuyUtility:
                UtilityTile boughtUtility = tiles[index].GetComponent<UtilityTile>();

                GivePlayerUtility(currPlayer, index);
                Debug.Log($"Bought utility {index}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - boughtUtility.PurchasePrice}"); // TODO: Delete once hands implemented
                players[currPlayer].GetComponent<Player>().money -= boughtUtility.PurchasePrice;

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
                price *= 8;

                Debug.Log($"Selected railroad tile {index}. Money {players[currPlayer].GetComponent<Player>().money} => {players[currPlayer].GetComponent<Player>().money - price}"); // TODO: Delete once hands implemented
                player.money -= price;
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[index]));

                ChangeState(GameState.RollDice);
                break;
            case GameState.PokemonCenter:
                PropertyTile freeUpgradedTile = tiles[index].GetComponent<PropertyTile>();
                Debug.Log($"Upgraded tile {index} for free. Property goes from level {freeUpgradedTile.Level} to level {freeUpgradedTile.Level + 1}"); // TODO: Delete once hands implemented

                freeUpgradedTile.Level += 1;

                ChangeState(GameState.RollDice);
                break;
            case GameState.TeamRocket:
                Debug.Log($"Stole tile {index}. Property goes from player {tiles[index].GetComponent<PropertyTile>().Owner} to player {currPlayer}"); // TODO: Delete once hands implemented
                GivePlayerProperty(currPlayer, index);

                ChangeState(GameState.RollDice);
                break;
        }
    }

    public static void SkipButtonPressed()
    {
        // All states change state to roll dice
        if (skipButton.GetComponent<SpriteRenderer>().enabled)
        {
            ChangeState(GameState.RollDice);
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
        if (players[currPlayer].GetComponent<Player>().money >= tile.PurchasePrice * tile.Level)
        {
            tiles[index].GetComponent<PropertyTile>().CanSelect = true;
        }
    }

    public static void PayPropertyRoutine(int propertyIndex)
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
            players[tile.Owner].GetComponent<Player>().money += cost;
            Debug.Log($"Player {currPlayer} has ${player.money}, and player {tile.Owner} has ${players[tile.Owner].GetComponent<Player>().money}"); // TODO: Delete once hands implemented
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
                price *= 8;

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

    public static void BuyUtilityRoutine(int utilityIndex)
    {
        ChangeState(GameState.BuyUtility);

        // Check if the tile costs more money than the player has and only allow selection if it doesn't
        UtilityTile tile = tiles[utilityIndex].GetComponent<UtilityTile>();
        if (players[currPlayer].GetComponent<Player>().money >= tile.PurchasePrice)
        {
            tiles[utilityIndex].GetComponent<UtilityTile>().CanSelect = true;
        }
    }

    public static void PayUtilityRoutine(int utilityIndex)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        UtilityTile tile = tiles[utilityIndex].GetComponent<UtilityTile>();
        Debug.Log($"Player {currPlayer} landed on utility owned by player {tile.Owner}"); // TODO: Delete once hands implemented
        if (tile.Owner != currPlayer)
        {
            int roll1 = dice[0].GetComponent<Dice>().GetResult();
            int roll2 = dice[1].GetComponent<Dice>().GetResult();
            int multiplier;

            if (tile.FullSet)
            {
                multiplier = 10;
            }
            else
            {
                multiplier = 4;
            }    

            int cost = (roll1 + roll2) * multiplier;

            player.money -= cost;
            players[tile.Owner].GetComponent<Player>().money += cost;
            Debug.Log($"Player {currPlayer} has ${player.money}, and player {tile.Owner} has ${players[tile.Owner].GetComponent<Player>().money}"); // TODO: Delete once hands implemented
            if (player.money < 0)
            {
                BankruptCurrentPlayer(tile.Owner);
            }
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
        if (player.position + dist >= 40)
        {
            player.money += 200;
        }
        board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[(player.position + dist) % 40]));

        // Call functionality for landing on a tile
        tiles[player.position].GetComponent<BasicTile>().OnLand();
    }

    /*
     * PRIVATE HELPER FUNCTIONS
     */

    // Handles giving a player property and removing/setting set bonuses
    // Note: ALWAYS CALL THIS FUNCTION WHEN CHANGING PROPERTY OWNERSHIP
    private static void GivePlayerProperty(int player, int propertyIndex)
    {
        PropertyTile property = tiles[propertyIndex].GetComponent<PropertyTile>();

        bool ownsFullSet = true;

        List<PropertyTile> typeSet = new List<PropertyTile>();

        property.Owner = player;

        foreach (GameObject tile in tiles)
        {
            PropertyTile otherProperty = tile.GetComponent<PropertyTile>();
            if (otherProperty != null) // If tile is actually a property
            {
                if (otherProperty.Type == property.Type)
                {
                    otherProperty.FullSet = false;
                    typeSet.Add(otherProperty);
                    if (otherProperty.Owner != player) ownsFullSet = false;
                }
            }
        }

        // If player doesn't own the full set, we don't set properties to FullSet. Banks also can't own them.
        if (!ownsFullSet || player == -1) return;

        Debug.Log($"Player {player} owns full property set of type {property.Type}");

        foreach (PropertyTile setTile in typeSet)
        {
            setTile.FullSet = true;
        }
    }

    private static void GivePlayerUtility(int player, int utilityIndex)
    {
        UtilityTile utility = tiles[utilityIndex].GetComponent<UtilityTile>();
        utility.Owner = player;

        bool ownsFullSet = true;

        List<UtilityTile> utilitySet = new List<UtilityTile>();

        foreach (GameObject tile in tiles)
        {
            UtilityTile otherUtility = tile.GetComponent<UtilityTile>();
            if (otherUtility != null) // If tile is actually a utility
            {
                otherUtility.FullSet = false;
                utilitySet.Add(otherUtility);
                if (otherUtility.Owner != player) ownsFullSet = false;
            }
        }

        // If player doesn't own the full set, we don't set properties to FullSet. Banks also can't own them.
        if (!ownsFullSet || player == -1) return;

        Debug.Log($"Player {player} owns full utility set");

        foreach (UtilityTile setTile in utilitySet)
        {
            setTile.FullSet = true;
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
                    GivePlayerProperty(debtedPlayer, property.index);
                    Debug.Log($"Transfered property ${property.index} from player {currPlayer} to {debtedPlayer}");
                }
            }
        }
        players[currPlayer].GetComponent<Player>().DestroyPlayer();
        // TODO: check if only 1 player is left and finish the game if so
        int numBankrupt = 0;

        foreach (GameObject playerObj in players)
        {
            Player player = playerObj.GetComponent<Player>();
            if (numBankrupt == players.Count - 1)
            {
                ChangeState(GameState.GameEnd);
            }
        }
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
        // Once game has ended, game state is locked, scene has to be reloaded to play again
        if (state == GameState.GameEnd) return;

        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<BasicTile>().CanSelect = false;
        }
        foreach (GameObject die in dice)
        {
            die.GetComponent<Dice>().actionable = false;
        }
        skipButton.GetComponent<SpriteRenderer>().enabled = false;

        switch (toState)
        {
            case GameState.GameEnd:
                // TODO: Show clickable UI for game end/play coroutine animation
                break;
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
            case GameState.BuyUtility:
            case GameState.UpgradeProperty:
            case GameState.Railroad:
                skipButton.GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
        
        state = toState;
    }
}