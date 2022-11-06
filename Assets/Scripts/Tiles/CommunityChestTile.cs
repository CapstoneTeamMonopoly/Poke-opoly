using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityChestTile : BasicTile
{ 
    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, GameManager.EndTileRoutine() must be called
    public override void OnLand()
    {
        Debug.Log("Landed on community chest");

        GameManager.EndTileRoutine();
    }
}

