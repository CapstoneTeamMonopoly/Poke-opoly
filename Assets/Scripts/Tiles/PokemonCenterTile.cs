using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonCenterTile : BasicTile
{
    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, a GameManager routine must be called.
    public override void OnLand()
    {
        Debug.Log($"Landed on the pokemon center: {index}");
        GameManager.PokemonCenterRoutine();
    }
}
