using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyTile : BasicTile
{
    public int Owner { get; set; } // Keeps track of the owner
    public string Type { get; set; } // The type of this tile, tracks owning sets
    public int PurchasePrice { get; set; }  // Price to be paid when purchasing the tile.
    public int BaseLandingPrice { get; set; }  // Price to be paid when landing on the tile.
    public int Level { get; set; }  // Adjusts the base landing price
    public bool FullSet { get; set; }  // Keeps track of whether 1 player owns all of this color

    public PropertyTile()
    {
        Owner = -1; // Owner is -1 if no player owns
        Level = 1; // By default evolution 1 (Level 1-3 is valid)
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, a GameManager routine must be called.
    public override void OnLand()
    {
        Debug.Log($"Landed on a property: {Owner}");
        if (Owner == -1) {
            GameManager.BuyPropertyRoutine(index);
        } 
        else
        {
            GameManager.PayPropertyRoutine(index);
        }
    }

    // TODO: Render three dots based off of the level to signify the evolution
}
