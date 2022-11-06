using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTile : BasicTile
{
    public int Owner { get; set; }
    public int PurchasePrice { get; set; }
    public bool FullSet { get; set; }

    public UtilityTile()
    {
        Owner = -1; // Owner is -1 if no player owns
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, a GameManager routine must be called.
    public override void OnLand()
    {
        Debug.Log($"Landed on a utility: {index}");
        if (Owner == -1)
        {
            GameManager.BuyUtilityRoutine(index);
        }
        else
        {
            GameManager.PayUtilityRoutine(index);
        }
    }
}
