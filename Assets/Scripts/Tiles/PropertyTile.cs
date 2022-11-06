using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyTile : BasicTile
{
    public int Owner { get; set; } // Keeps track of the owner
    //public string Color { get; set; } // The color of this tile
    public int PurchasePrice { get; set; }  // Price to be paid when purchasing the tile.
    public int BaseLandingPrice { get; set; }  // Price to be paid when landing on the tile.
    public int Level { get; set; }  // Adjusts the base landing price
    public bool FullSet { get; set; }  // Keeps track of whether 1 player owns all of this color

    public PropertyTile()
    {
        Owner = -1; // Owner is -1 if no player owns
        Level = 0; // By default evolution 1 (Level 0-3 is valid, 0 for when not owned)
    }

    public override void OnLand()
    {
        Debug.Log($"Landed on a property: {Owner}");
        if (Owner == -1) {
            GameManager.BuyPropertyRoutine(index);
        } 
        else
        {
            GameManager.PayOnLand(index);
        }
    }

    // TODO: Render three dots based off of the level to signify the evolution
}
