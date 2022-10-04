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
        CanSelect = true;
        // Action goes here
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // Action goes here
    }
}
