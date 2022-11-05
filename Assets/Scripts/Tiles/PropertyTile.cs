using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyTile : BasicTile
{
    public int Owner { get; set; } // Keeps track of the owner
    //public string Color { get; set; } // The color of this tile
    public int Purchase_Price { get; set; }  // Price to be paid when purchasing the tile.
    public int Base_Landing_Price { get; set; }  // Price to be paid when landing on the tile.
    public int Level { get; set; }  // Adjusts the base landing price
    public int Full_Set { get; set; }  // Keeps track of whether 1 player owns all of this color

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

    private void OnMouseDown()
    {
        if (CanSelect)
        {
            // Function will then need interact with the board to tell it that it's been chosen, and the board will then call the relevant function it expects when the tile is clicked on
        }
    }
}
