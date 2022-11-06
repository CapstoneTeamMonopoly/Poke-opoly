using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailroadTile : ActionTile
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // Action goes here
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // Action goes here
    }


    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, GameManager.EndTileRoutine() must be called
    public override void OnLand()
    {
        Debug.Log("Landed on a railroad");
        GameManager.RailroadRoutine(index);

        GameManager.EndTileRoutine();
    }
}

