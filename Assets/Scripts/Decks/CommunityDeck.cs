using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityDeck : Deck
{
    public void Start()
    {
        base.Start();
        numOptions = 17; // Number of cards in the deck!
    }

    public void Update()
    {
        base.Update();
    }

    private void OnMouseDown()
    {
        if (CanSelect) GameManager.DrawComCard();
    }

    public void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles, ref GameObject board, ref EventHandler handler)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        switch (card)
        {
            case 0:
                Debug.Log($"Card 0: Send player{player.id} to Go, give 200.");
                player.ChangeBalance(200);
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[0]));
                DrawOver();
                break;
            case 1:
                Debug.Log($"Card 1: Player {player.id} recieves 200");
                player.ChangeBalance(200);
                DrawOver();
                break;
            case 2:
                Debug.Log($"Card 2: Player {player.id} loses 50");
                player.ChangeBalance(-50);
                DrawOver();
                break;
            case 3:
                Debug.Log($"Card 3: Player {player.id} gains 50");
                player.ChangeBalance(50);
                DrawOver();
                break;
            case 4:
                Debug.Log($"Card 4: Trigger Pokemon Center");
                foreach (GameObject tile in tiles)
                {
                    PokemonCenterTile tileScript = tile.GetComponent<PokemonCenterTile>();
                    if (tileScript != null)
                    {
                        GameManager.PlayerLand(tileScript.index);
                        break;
                    }
                }
                break;
            case 5:
                Debug.Log($"Card 5: Trigger Team Rocket");
                foreach (GameObject tile in tiles)
                {
                    TeamRocketTile tileScript = tile.GetComponent<TeamRocketTile>();
                    if (tileScript != null)
                    {
                        GameManager.PlayerLand(tileScript.index);
                        break;
                    }
                }
                break;
            case 6:
                Debug.Log($"Card 6: Player {player.id} takes 50 from each other player");
                if (player.id != 0)
                    players[0].GetComponent<Player>().ChangeBalance(-50);
                    Debug.Log($"50 taken from player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().ChangeBalance(-50);
                    Debug.Log($"50 taken from player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().ChangeBalance(-50);
                    Debug.Log($"50 taken from player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().ChangeBalance(-50);
                    Debug.Log($"50 taken from player 3");

                Debug.Log($"Player {player.id} recieves 150");
                player.ChangeBalance(150);
                DrawOver();
                break;
            case 7:
                Debug.Log($"Card 7: Player {player.id} recieves 100");
                player.ChangeBalance(100);
                DrawOver();
                break;
            case 8:
                Debug.Log($"Card 8: Player {player.id} recieves 20");
                player.ChangeBalance(20);
                DrawOver();
                break;
            case 9:
                Debug.Log($"Card 9: Player {player.id} takes 10 from each other player");
                if (player.id != 0)
                    players[0].GetComponent<Player>().ChangeBalance(-10);
                    Debug.Log($"10 taken from player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().ChangeBalance(-10);
                    Debug.Log($"10 taken from player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().ChangeBalance(-10);
                    Debug.Log($"10 taken from player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().ChangeBalance(-10);
                    Debug.Log($"10 taken from player 3");

                Debug.Log($"Player {player.id} recieves 30");
                player.ChangeBalance(30);
                DrawOver();
                break;
            case 10:
                Debug.Log($"Card 10: Player {player.id} recieves 100");
                player.ChangeBalance(100);
                DrawOver();
                break;
            case 11:
                Debug.Log($"Card 11: Player {player.id} loses 50");
                player.ChangeBalance(-50);
                DrawOver();
                break;
            case 12:
                Debug.Log($"Card 12: Player {player.id} loses 50");
                player.ChangeBalance(-50);
                DrawOver();
                break;
            case 13:
                Debug.Log($"Card 13: Player {player.id} recieves 25");
                player.ChangeBalance(25);
                DrawOver();
                break;
            case 14:
                // These tile ids refer to properties.
                int price = 0;

                Debug.Log($"Card 14: Charge player {player.id} based on properties owned.");
                foreach (GameObject tile in tiles)
                {
                    PropertyTile property = tile.GetComponent<PropertyTile>();
                    if (property != null)
                    {
                        if (tiles[property.index].GetComponent<PropertyTile>().Owner == currPlayer)
                        {
                            if (tiles[property.index].GetComponent<PropertyTile>().Level == 3)
                                price += 45;
                            else
                                price += 10 * tiles[property.index].GetComponent<PropertyTile>().Level;
                        }
                    }
                }
                player.ChangeBalance(-price);
                DrawOver();
                break;
            case 15:
                Debug.Log($"Card 15: Player {player.id} recieves 10");
                player.ChangeBalance(10);
                DrawOver();
                break;
            case 16:
                Debug.Log($"Card 16: Player {player.id} recieves 100");
                player.ChangeBalance(100);
                DrawOver();
                break;
        }
    }
}

