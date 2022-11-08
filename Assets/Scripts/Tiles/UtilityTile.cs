using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityTile : BasicTile
{
    public int Owner { get; private set; }
    public int PurchasePrice { get; set; }
    public bool FullSet { get; set; }

    private GameObject tileOwner;

    public UtilityTile()
    {
        Owner = -1; // Owner is -1 if no player owns
    }

    public override void Start()
    {
        base.Start();

        tileOwner = new GameObject($"owner-{index}", typeof(SpriteRenderer));
        tileOwner.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Called when a player lands on this tile, starts tile functionality. At the end of each OnLand() function, a GameManager routine must be called.
    public override void OnLand()
    {
        Debug.Log($"Landed on a utility: {index}");
        if (Owner == -1)
        {
            GameManager.BuyUtilityRoutine(index);
        }
        else
        {
            GameManager.PayUtilityRoutine(index);
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
