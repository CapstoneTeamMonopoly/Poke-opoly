using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    public void HandleInteraction(GameManager.GameState state)
    {
        switch (state)
        {
            // No action needed
            case GameManager.GameState.GameEnd:
                break;
            // No logic needed
            case GameManager.GameState.RollDice:
                GameManager.RollDice();
                break;
            case GameManager.GameState.DrawChanceCard:
                GameManager.DrawChanceCard();
                break;
            case GameManager.GameState.DrawComCard:
                GameManager.DrawComCard();
                break;
            // Calls helper functions, which choose action to take and calls functions TileClicked() and SkipButtonPressed()
            case GameManager.GameState.BuyProperty:
            case GameManager.GameState.BuyUtility:
            case GameManager.GameState.UpgradeProperty:
            case GameManager.GameState.Railroad:
            case GameManager.GameState.PokemonCenter:
            case GameManager.GameState.TeamRocket:
                List<GameObject> selectableTiles = GameManager.GetSelectableTiles();

                if (selectableTiles.Count != 0)
                {
                    int choice = Random.Range(0, selectableTiles.Count - 1);
                    Debug.Log($"AI Choice: {selectableTiles[choice].GetComponent<BasicTile>().index}");
                    GameManager.TileClicked(selectableTiles[choice].GetComponent<BasicTile>().index);
                }
                else
                {
                    GameManager.SkipButtonPressed();
                }
                break;
        }
    }
}
