using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunityDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {
        numOptions = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override void OnMouseDown()
    {
        if (CanSelect)
            GameManager.ComCardTrigger();
    }

    void Effect(int card, int currPlayer, ref List<GameObject> players, ref List<GameObject> tiles)
    {
        switch (card)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }

}

