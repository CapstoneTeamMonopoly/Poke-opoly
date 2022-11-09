using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    /*
     * Include coroutines here to be run by creating a <Type>Event which inherents Event
     */

    public IEnumerator StartRollEvent(GameObject dice1, GameObject dice2)
    {
        RollEvent newEvent = new RollEvent(dice1, dice2);
        yield return StartCoroutine(newEvent.RunEvent());
        int roll1 = dice1.GetComponent<Dice>().GetResult();
        int roll2 = dice2.GetComponent<Dice>().GetResult();
        if (roll1 == roll2)
        {
            GameManager.doubles = true;
        }
        GameManager.MovePlayerByDice(roll1 + roll2);
    }

    public IEnumerator MovePlayerTo(GameObject player, GameObject dest)
    {
        MoveEvent newEvent = new MoveEvent(player, dest);
        yield return StartCoroutine(newEvent.RunEvent());
        GameManager.PlayerLand(player.GetComponent<Player>().position);
    }

    public IEnumerator MovePlayerNoLand(GameObject player, GameObject dest)
    {
        MoveEvent newEvent = new MoveEvent(player, dest);
        yield return StartCoroutine(newEvent.RunEvent());
    }

    public IEnumerator UpdateMoney(Player p, GameObject board, int src, int dest)
    {
        MoneyEvent newEvent = new MoneyEvent(p, board.GetComponent<Board>(), src, dest);
        yield return StartCoroutine(newEvent.RunEvent());
    }
}