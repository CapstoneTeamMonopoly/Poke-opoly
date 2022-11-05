using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static int currPlayer;

    private static List<GameObject> players;
    private static List<GameObject> tiles;
    private static List<GameObject> dice;
    // Community chest and other deck should be here

    public enum GameState
    {
        RollDice,
        PlayerAction,
        DrawCard,
    }

    private static GameState state;

    public static void InitGameObjects(List<GameObject> players, List<GameObject> tiles, List<GameObject> dice)
    {
        GameManager.players = players;
        GameManager.tiles = tiles;
        GameManager.dice = dice;
    }

    // SetState does the action of the current state, moves to the next state, and sets up that state
    public static void SetState(GameState toState)
    {
        switch (state)
        {
            case GameState.RollDice:
                //MovePlayer();
                break;
            case GameState.PlayerAction:
                break;
            case GameState.DrawCard:
                break;
        }
        // TODO: Switch statement does the one time functionality when states are switched
        // e.g. SetState(GameState.RollDice) should tell the dice that they are ready to be clicked
        switch (toState)
        {
            case GameState.RollDice:
                foreach (GameObject die in dice)
                {
                    die.GetComponent<Dice>().actionable = true;
                }
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
}
