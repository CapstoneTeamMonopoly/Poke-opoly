using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObj : MonoBehaviour
{
    // Start is called before the first frame update.
    public abstract void Start();

    // Update is called once per frame.
    public abstract void Update();

    // OnClicked defines action for when a tile is clicked.
    public abstract void OnClicked();
}

public class BasicTile : TileObj
{
    /*
     *  BasicTile should handle all the standard visual elements of each tile (changing size when clicked on, hovered over, etc.
     */

    // CanSelect determines whether a tile is selectable as a button by the player
    public bool CanSelect { get; set; }

    private bool isScaled; 

    private Vector3 actionableScale;
    private Vector3 clickScale;

    // Start is called before the first frame update
    public override void Start()
    {
        isScaled = false;
        actionableScale = new Vector3(1f, 1f, 1f);
        clickScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (CanSelect && !isScaled)
        {
            isScaled = true;
            this.transform.localScale += actionableScale;
        } 
        if (!CanSelect && isScaled)
        {
            isScaled = false;
            this.transform.localScale -= actionableScale;
        }
    }

    public override void OnClicked()
    {
        
    }

    private void OnMouseDown()
    {
        if (CanSelect)
        {
            this.transform.localScale += clickScale;
            // TODO: Need to figure out how to wait time before scaling back here
            this.transform.localScale -= clickScale;
        }
    }
}
