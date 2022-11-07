using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public bool CanSelect {get; set;}
    private List<int> cards;
    public int numOptions;

    private GameObject deckSelectable;

    public void Start()
    {
        deckSelectable = new GameObject($"deckSelector", typeof(SpriteRenderer));
        deckSelectable.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/tileSelectable");

        deckSelectable.transform.localScale = transform.localScale;
        deckSelectable.transform.position = transform.position + new Vector3(0, 0, -1);

        CanSelect = false;
        cards = new List<int>();
        Shuffle();
    }

    public void Update()
    {
        deckSelectable.GetComponent<SpriteRenderer>().enabled = CanSelect;
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

    public void DrawOver()
    {
        GameManager.EndActionRoutine();
    }
}
