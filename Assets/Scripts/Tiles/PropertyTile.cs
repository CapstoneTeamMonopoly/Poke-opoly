using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyTile : BasicTile
{
    public int Owner { get; private set; } // Keeps track of the owner
    public string Type { get; set; } // The type of this tile, tracks owning sets
    public int PurchasePrice { get; set; }  // Price to be paid when purchasing the tile.
    public int BaseLandingPrice { get; set; }  // Price to be paid when landing on the tile.
    public int Level { get; private set; }  // Adjusts the base landing price
    public bool FullSet { get; set; }  // Keeps track of whether 1 player owns all of this color

    private string spritePath;
    private GameObject tileOwner;

    public PropertyTile()
    {
        Owner = -1; // Owner is -1 if no player owns
        Level = 1; // By default evolution 1 (Level 1-3 is valid)
    }

    public override void Start()
    {
        base.Start();

        tileOwner = new GameObject($"owner-{index}", typeof(SpriteRenderer));
        tileOwner.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetBaseSprite(string path)
    {
        spritePath = path;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"{spritePath}-e{Level}");
    }

    public void Evolve()
    {
        Level += 1;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"{spritePath}-e{Level}");
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

    public void SetOwner(int player)
    {
        Owner = player;

        if (Owner == -1)
        {
            tileOwner.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            tileOwner.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Board/p{Owner}-owned");
            tileOwner.GetComponent<SpriteRenderer>().enabled = true;
            tileOwner.transform.position = gameObject.transform.position + new Vector3(0, 0, -0.5f);
            tileOwner.transform.localScale = gameObject.transform.localScale;
        }
    }
}
