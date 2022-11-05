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
        GameManager.MovePlayer(roll1 + roll2);
    }
}
