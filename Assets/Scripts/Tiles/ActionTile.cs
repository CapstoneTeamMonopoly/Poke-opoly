using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTile : BasicTile
{
    private void OnMouseDown()
    {
        if (CanSelect)
        {
            Debug.Log("Clicked");
            // Function will then need interact with the board to tell it that it's been chosen, and the board will then call the relevant function it expects when the tile is clicked on
        }
    }
}
