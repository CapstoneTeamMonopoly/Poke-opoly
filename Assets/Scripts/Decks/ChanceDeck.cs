using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {
        numOptions = 14;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override void OnMouseDown()
    {
        if (CanSelect)
            GameManager.ComCardTrigger();
    }

    void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles)
    {
        Player player = players[currPlayer].GetComponent<Player>();
        switch (card)
        {
            case 0:
                Debug.Log($"Card 0: Send player{player.id} to Go, give 200.");
                player.money += 200;
                player.MovePlayer(tiles[0]);
                player.position = 0;
                break;
            case 1:
                Debug.Log($"Card 1: Send player {player.id} to tile 24, give 200 if they pass go.");
                if (player.position > 24)
                    player.money += 200;
                    Debug.Log($"200 awarded.");
                player.MovePlayer(tiles[24]);
                player.position = 24;
                break;
            case 2:
                Debug.Log($"Card 2: Send player {player.id} to tile 11, give 200 if they pass go.");
                if (player.position > 11)
                    player.money += 200;
                    Debug.Log($"200 awarded.");
                player.MovePlayer(tiles[11]);
                player.position = 11;
                break;
            case 3:
                Debug.Log($"Card 3: Send player {player.id} to next Utility, trigger on-land effect.");
                int target = 0;
                if (player.position < 12 || player.position >= 28)
                    target = 12;
                else if (player.position < 28)
                    target = 28;
                
                Debug.Log($"Sent player to tile {target}.");
                player.MovePlayer(tiles[target]);
                player.position = target;
                tiles[target].GetComponent<BasicTile>().OnLand();
                break;
            case 4:
                Debug.Log($"Card 4: Send player {player.id} to next railroad, trigger on-land effect.");
                int target = 0;
                if (player.position < 5 || player.position >= 35)
                    target = 5;
                else if (player.position < 15)
                    target = 15;
                else if (player.position < 25)
                    target = 25;
                else if (player.position < 35)
                    target = 35;

                Debug.Log($"Sent player to tile {target}.");
                player.MovePlayer(tiles[target]);
                player.position = target;
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
                int target = player.position - 3;
                if (target < 0)
                    target += 40;
                Debug.Log($"Moving player from tile {player.position} to tile {target}");
                player.MovePlayer(target);
                player.position = target;
                break;
            case 8:
                Debug.Log($"Card 8: Trigger Team Rocket");
                tiles[30].GetComponent<BasicTile>().OnLand();
                break;
            case 9:
                
        }
    }
}
