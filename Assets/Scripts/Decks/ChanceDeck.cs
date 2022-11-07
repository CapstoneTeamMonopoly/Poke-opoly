using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {
        numOptions = 14; // Number of cards in the deck!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (CanSelect)
            GameManager.DrawChanceCard();
    }

    public void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles, ref GameObject board, ref EventHandler handler)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        int target;
        switch (card)
        {
            case 0:
                Debug.Log($"Card 0: Send player{player.id} to Go, give 200.");
                player.money += 200;
                board.GetComponent<Board>().StartCoroutine(handler.GetComponent<EventHandler>().MovePlayerTo(players[currPlayer], tiles[0]));
                break;
            case 1:
                Debug.Log($"Card 1: Send player {player.id} to tile 24, give 200 if they pass go.");
                if (player.position > 24)
                {
                    player.money += 200;
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[24]));
                break;
            case 2:
                Debug.Log($"Card 2: Send player {player.id} to tile 11, give 200 if they pass go.");
                if (player.position > 11)
                {
                    player.money += 200;
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[11]));
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
                tiles[target].GetComponent<BasicTile>().OnLand();
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
                tiles[target].GetComponent<BasicTile>().OnLand();
                break;
            case 5:
                Debug.Log($"Card 5: Player {player.id} recieves $50");
                player.money += 50;
                break;
            case 6:
                Debug.Log($"Card 6: Trigger Pokemon Center");
                tiles[10].GetComponent<BasicTile>().OnLand();
                break;
            case 7:
                Debug.Log($"Card 7: Player {player.id} is moved back 3 spaces.");
                target = player.position - 3;
                if (target < 0)
                    target += 40;
                Debug.Log($"Moving player from tile {player.position} to tile {target}");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[target]));
                break;
            case 8:
                Debug.Log($"Card 8: Trigger Team Rocket");
                tiles[30].GetComponent<BasicTile>().OnLand();
                break;
            case 9:
                // These tile ids refer to properties.
                int[] properties = {1, 3, 6, 8, 9, 11, 13, 14, 16, 18, 19, 21, 23, 24, 26, 27, 29, 31, 32, 34, 37, 39};
                int price = 0;

                Debug.Log($"Card 9: Charge player {player.id} based on properties owned.");
                for(int i = 0; i < properties.Length; i++)
                {
                    if (tiles[properties[i]].GetComponent<PropertyTile>().Owner == currPlayer)
                    {
                        if (tiles[properties[i]].GetComponent<PropertyTile>().Level == 3)
                            price += 45;
                        else
                            price += 10 * tiles[properties[i]].GetComponent<PropertyTile>().Level;
                    }
                }
                player.money -= price;
                break;
            case 10:
                Debug.Log($"Card 10: Send player {player.id} to tile 5, give 200 if they pass go");
                if (player.position > 5)
                {
                    player.money += 200;
                    Debug.Log($"200 awarded.");
                }
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[5]));
                break;
            case 11:
                Debug.Log($"Card 11: Send player {player.id} to tile 39");
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[39]));
                break;
            case 12:
                Debug.Log($"Card 12: Award players other than player {player.id}");
                if (player.id != 0)
                    players[0].GetComponent<Player>().money += 50;
                    Debug.Log($"Awarded 50 to player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().money += 50;
                    Debug.Log($"Awarded 50 to player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().money += 50;
                    Debug.Log($"Awarded 50 to player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().money += 50;
                    Debug.Log($"Awarded 50 to player 3");
                break;
            case 13:
                Debug.Log($"Card 13: Player {player.id} recieves 150");
                player.money += 150;
                break;

        }
    }
}
