using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicTile : TileObj
{
    /*
     *  BasicTile should handle all the standard visual elements of each tile (changing size when clicked on, hovered over, etc.
     */

    // CanSelect determines whether a tile is selectable as a button by the player
    public bool CanSelect { get; set; }

    private bool isScaled;

    private Vector3 actionableScale;
    private Vector3 hoverScale;

    // Start is called before the first frame update
    public override void Start()
    {
        isScaled = false;
        actionableScale = new Vector3(0.75f, 0.75f, 0f);
        hoverScale = new Vector3(0.3f, 0.3f, 0f);
        Debug.Log("Tile Created");
    }

    // Update is called once per frame
    public override void Update()
    {
        if (CanSelect && !isScaled)
        {
            isScaled = true;
            this.transform.localScale += actionableScale;
            Debug.Log("Actionable Scale");
        }
        if (!CanSelect && isScaled)
        {
            isScaled = false;
            this.transform.localScale -= actionableScale;
            Debug.Log("Actionable Scale Back");
        }
    }

    private void OnMouseEnter()
    {
        this.transform.localScale += hoverScale;
        Debug.Log("Mouse enter");
    }

    private void OnMouseExit()
    {
        this.transform.localScale -= hoverScale;
        Debug.Log("Mouse exit");
    }
}
