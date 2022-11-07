using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {
        numOptions = 17; // Number of cards in the deck!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (CanSelect)
            GameManager.DrawComCard();
    }

    public void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles, ref GameObject board, ref EventHandler handler)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        switch (card)
        {
            case 0:
                Debug.Log($"Card 0: Send player{player.id} to Go, give 200.");
                player.money += 200;
                board.GetComponent<Board>().StartCoroutine(handler.MovePlayerTo(players[currPlayer], tiles[0]));
                break;
            case 1:
                Debug.Log($"Card 1: Player {player.id} recieves 200");
                player.money += 200;
                break;
            case 2:
                Debug.Log($"Card 2: Player {player.id} loses 50");
                player.money -= 50;
                break;
            case 3:
                Debug.Log($"Card 3: Player {player.id} gains 50");
                player.money += 50;
                break;
            case 4:
                Debug.Log($"Card 4: Trigger Pokemon Center");
                tiles[10].GetComponent<BasicTile>().OnLand();
                break;
            case 5:
                Debug.Log($"Card 5: Trigger Team Rocket");
                tiles[30].GetComponent<BasicTile>().OnLand();
                break;
            case 6:
                Debug.Log($"Card 6: Player {player.id} takes 50 from each other player");
                if (player.id != 0)
                    players[0].GetComponent<Player>().money -= 50;
                    Debug.Log($"50 taken from player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().money -= 50;
                    Debug.Log($"50 taken from player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().money -= 50;
                    Debug.Log($"50 taken from player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().money -= 50;
                    Debug.Log($"50 taken from player 3");

                Debug.Log($"Player {player.id} recieves 150");
                player.money += 150;
                break;
            case 7:
                Debug.Log($"Card 7: Player {player.id} recieves 100");
                player.money += 100;
                break;
            case 8:
                Debug.Log($"Card 8: Player {player.id} recieves 20");
                player.money += 20;
                break;
            case 9:
                Debug.Log($"Card 9: Player {player.id} takes 10 from each other player");
                if (player.id != 0)
                    players[0].GetComponent<Player>().money -= 10;
                    Debug.Log($"10 taken from player 0");
                if (player.id != 1)
                    players[1].GetComponent<Player>().money -= 10;
                    Debug.Log($"10 taken from player 1");
                if (player.id != 2)
                    players[2].GetComponent<Player>().money -= 10;
                    Debug.Log($"10 taken from player 2");
                if (player.id != 3)
                    players[3].GetComponent<Player>().money -= 10;
                    Debug.Log($"10 taken from player 3");

                Debug.Log($"Player {player.id} recieves 30");
                player.money += 30;
                break;
            case 10:
                Debug.Log($"Card 10: Player {player.id} recieves 100");
                player.money += 100;
                break;
            case 11:
                Debug.Log($"Card 11: Player {player.id} loses 50");
                player.money -= 50;
                break;
            case 12:
                Debug.Log($"Card 12: Player {player.id} loses 50");
                player.money -= 50;
                break;
            case 13:
                Debug.Log($"Card 13: Player {player.id} recieves 25");
                player.money += 25;
                break;
            case 14:
                // These tile ids refer to properties.
                int[] properties = {1, 3, 6, 8, 9, 11, 13, 14, 16, 18, 19, 21, 23, 24, 26, 27, 29, 31, 32, 34, 37, 39};
                int price = 0;

                Debug.Log($"Card 14: Charge player {player.id} based on properties owned.");
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
            case 15:
                Debug.Log($"Card 15: Player {player.id} recieves 10");
                player.money += 10;
                break;
            case 16:
                Debug.Log($"Card 16: Player {player.id} recieves 100");
                player.money += 100;
                break;

        }
    }

}

