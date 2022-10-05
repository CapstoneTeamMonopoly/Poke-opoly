using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyTile : BasicTile
{
    public int Owner { get; set; }

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
            Debug.Log("Clicked");
            // Function will then need interact with the board to tell it that it's been chosen, and the board will then call the relevant function it expects when the tile is clicked on
        }
    }
}
