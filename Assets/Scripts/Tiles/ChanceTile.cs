using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceTile : BasicTile
{
    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, GameManager.EndTileRoutine() must be called
    public override void OnLand()
    {
        Debug.Log("Landed on a chance tile");
        GameManager.ChanceRoutine();

        GameManager.EndTileRoutine();
    }
}