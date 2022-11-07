using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceDeck : Deck
{
    public void Start()
    {
        base.Start();
        numOptions = 14; // Number of cards in the deck!
    }

    public void Update() 
    {
        base.Update();
    }

    private void OnMouseDown()
    {
        if (CanSelect) GameManager.DrawChanceCard();
    }

    public void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles, ref GameObject board, ref EventHandler handler)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        int target;
        switch (card)
        {
            case 0:
                Debug.Log($"Card 0: Send player{player.id} to Go, give 200.");
                player.ChangeBalance(200);
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[0]));
                DrawOver();
                break;
            case 1:
                Debug.Log($"Card 1: Send player {player.id} to tile 24, give 200 if they pass go.");
                if (player.position > 24)
                {
                    player.ChangeBalance(200);
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[24]));
                DrawOver();
                break;
            case 2:
                Debug.Log($"Card 2: Send player {player.id} to tile 11, give 200 if they pass go.");
                if (player.position > 11)
                {
                    player.ChangeBalance(200);
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[11]));
                DrawOver();
                break;
            case 3:
                Debug.Log($"Card 3: Send player {player.id} to next Utility, trigger on-land effect.");
                target = 0;
                if (player.position < 12 || player.position >= 28)
                    target = 12;
                else if (player.position < 28)
                    target = 28;
                
                Debug.Log($"Sent player to tile {target}.");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[target]));
                break;
            case 4:
                Debug.Log($"Card 4: Send player {player.id} to next railroad, trigger on-land effect.");
                target = 0;
                if (player.position < 5 || player.position >= 35)
                    target = 5;
                else if (player.position < 15)
                    target = 15;
                else if (player.position < 25)
                    target = 25;
                else if (player.position < 35)
                    target = 35;

                Debug.Log($"Sent player to tile {target}.");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[target]));
                break;
            case 5:
                Debug.Log($"Card 5: Player {player.id} recieves $50");
                player.ChangeBalance(50);
                DrawOver();
                break;
            case 6:
                Debug.Log($"Card 6: Trigger Pokemon Center");
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
            case 7:
                Debug.Log($"Card 7: Player {player.id} is moved back 3 spaces.");
                target = player.position - 3;
                if (target < 0)
                    target += 40;
                Debug.Log($"Moving player from tile {player.position} to tile {target}");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[target]));
                DrawOver();
                break;
            case 8:
                Debug.Log($"Card 8: Trigger Team Rocket");
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
            case 9:
                int price = 0;

                Debug.Log($"Card 9: Charge player {player.id} based on properties owned.");
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
            case 10:
                Debug.Log($"Card 10: Send player {player.id} to tile 5, give 200 if they pass go");
                if (player.position > 5)
                {
                    player.ChangeBalance(200);
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[5]));
                DrawOver();
                break;
            case 11:
                Debug.Log($"Card 11: Send player {player.id} to tile 39");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerNoLand(players[currPlayer], tiles[39]));
                DrawOver();
                break;
            case 12:
                Debug.Log($"Card 12: Award players other than player {player.id}");
                if (player.id != 0)
                    players[0].GetComponent<Player>().ChangeBalance(50);
                Debug.Log($"Awarded 50 to player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().ChangeBalance(50);
                Debug.Log($"Awarded 50 to player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().ChangeBalance(50);
                Debug.Log($"Awarded 50 to player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().ChangeBalance(50);
                Debug.Log($"Awarded 50 to player 3");
                DrawOver();
                break;
            case 13:
                Debug.Log($"Card 13: Player {player.id} recieves 150");
                player.ChangeBalance(150);
                DrawOver();
                break;
        }
    }
}
