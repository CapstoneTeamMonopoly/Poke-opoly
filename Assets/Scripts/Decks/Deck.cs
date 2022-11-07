using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public bool CanSelect {get; set;}
    private List<int> cards;
    public int numOptions;


    // Start is called before the first frame update
    void Start()
    {
        CanSelect = false;
        cards = new List<int>();
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log($"How'd you get here?");
    }

    private void Shuffle()
    {
        List<int> values = new List<int>();

        for(int i = 0; i < numOptions; i++)
            values.Add(i);

        while(values.Count > 0)
        {
            int i = Random.Range(0, values.Count);
            cards.Add(values[i]);
            values.RemoveAt(i);
        }
    }

    public int DrawCard()
    {
        if (cards.Count == 0)
            Shuffle();

        int card = cards[0];
        cards.RemoveAt(0);

        return card;
    }

}
