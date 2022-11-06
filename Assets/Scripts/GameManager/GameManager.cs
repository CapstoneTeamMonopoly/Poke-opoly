using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static int currPlayer;

    private static GameObject board;
    private static List<GameObject> players;
    private static List<GameObject> tiles;
    private static List<GameObject> dice;

    // Used to see if turn changes or if the same player goes twice
    public static bool doubles { get; set; }

    private static EventHandler handler;
    // Community chest and other deck should be here

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
            case GameState.PlayerAction:
                break;
            case GameState.DrawCard:
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

    public static void MovePlayer(int dist)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        player.position += dist;
        if (player.position >= 40)
        {
            // passed go, give money
        }
        player.position %= 40;
        player.MovePlayer(tiles[player.position]);

        // call functionality for landing on a tile
        tiles[player.position].GetComponent<BasicTile>().OnLand();
    }

    public static void TileClicked(int index)
    {

    }

    public static void EndTileRoutine()
    {
        ChangeState(GameState.RollDice);
    }
}
