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
        IEnumerator coroutine = newEvent.RunEvent();
        yield return StartCoroutine(coroutine);
        StopCoroutine(coroutine);
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
        IEnumerator coroutine = newEvent.RunEvent();
        yield return StartCoroutine(coroutine);
        StopCoroutine(coroutine);
    }
}