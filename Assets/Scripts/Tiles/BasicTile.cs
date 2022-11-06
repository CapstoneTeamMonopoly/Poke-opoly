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

    public override void Start() {}

    public override void Update() {}

    private void OnMouseDown()
    {
        Debug.Log("wah");
        if (CanSelect)
        {
            GameManager.TileClicked(index);
        }
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, GameManager.EndTileRoutine() must be called
    public virtual void OnLand()
    {
        Debug.Log("Landed on a basic tile");
        GameManager.EndTileRoutine();
    }
}
