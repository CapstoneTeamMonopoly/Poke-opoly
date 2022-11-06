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
    private bool selectShown;

    public override void Start() 
    {
        tileSelectable = new GameObject($"selector-{index}", typeof(SpriteRenderer));
        tileSelectable.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/tileSelectable");
    }

    public override void Update() 
    {
        tileSelectable.transform.localScale = transform.localScale;
        if (CanSelect)
        {
            tileSelectable.transform.position = transform.position + new Vector3(0, 0, -3);
        }
        else
        {
            tileSelectable.transform.position = transform.position + new Vector3(0, 0, 1);
        }    
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, GameManager.EndTileRoutine() must be called
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
}