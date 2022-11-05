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

    private static EventHandler handler;
    // Community chest and other deck should be here

    static GameManager()
    {
        currPlayer = -1;
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
                IncrementTurn();
                break;
            case GameState.PlayerAction:
                break;
            case GameState.DrawCard:
                break;
        }
        state = toState;
    }

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
        board.GetComponent<Board>().StartCoroutine(handler.StartRollEvent(dice[0], dice[1]));
    }

    public static void MovePlayer(int dist)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        player.position += dist;
        player.position %= 40;
        player.MovePlayer(tiles[player.position]);
    }

    public static void TileClicked(int index)
    {

    }

    /*
     *  Coroutines
     */
}
