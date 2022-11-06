using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public Event()
    {

    }

    public IEnumerator RunEvent()
    {
        yield return new WaitForEndOfFrame();
    }
}
