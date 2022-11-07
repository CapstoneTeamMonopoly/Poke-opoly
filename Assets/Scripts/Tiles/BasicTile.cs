using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicTile : TileObj
{
    /*
     *  BasicTile should handle all the standard visual elements of each tile (changing size hovered over and selectable)
     */

    // CanSelect determines whether a tile is selectable as a button by the player
    public bool CanSelect { get; set; }
    public int index { get; set; }

    private GameObject tileSelectable;

    private GameObject tileInfo;
    private bool hovering;


    public override void Start() 
    {
        tileSelectable = new GameObject($"tileSelector-{index}", typeof(SpriteRenderer));
        tileSelectable.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/tileSelectable");

        tileSelectable.transform.localScale = transform.localScale;
        tileSelectable.transform.position = transform.position + new Vector3(0, 0, -1);

        tileInfo = new GameObject($"tileInfo-{index}", typeof(SpriteRenderer));
        tileInfo.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        tileInfo.transform.localScale = transform.localScale * 4;
        tileInfo.transform.position = new Vector3(0, 0, -5);
    }

    // Override Update() to show tile info when hovering
    public override void Update() 
    {
        tileSelectable.GetComponent<SpriteRenderer>().enabled = CanSelect;
        tileInfo.GetComponent<SpriteRenderer>().enabled = hovering;
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, a GameManager routine must be called.
    public virtual void OnLand()
    {
        Debug.Log("Landed on a basic tile");
        GameManager.EndTileRoutine();
    }

    private void OnMouseDown()
    {
        if (CanSelect)
        {
            // Tell GameManager tile has been clicked
            GameManager.TileClicked(index);
        }
    }

    private void OnMouseOver()
    {
        if (CanSelect)
        {
            hovering = true;
        }
        else
        {
            hovering = false;
        }
    }

    private void OnMouseExit()
    {
        hovering = false;
    }
}