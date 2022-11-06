using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    private SpriteRenderer rend;

	// If the game state currently allows the dice to be rolled they are actionable
	public bool actionable { get; set; }

	// Result of dice roll
	private int result;

    private GameObject diceSelectable;

    public int GetResult()
    {
        return result;
    }

	// Use this for initialization
	void Start()
    {
        diceSelectable = new GameObject("diceSelector", typeof(SpriteRenderer));
        diceSelectable.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Dice/diceSelectable");

        // Assign Renderer component
        rend = GetComponent<SpriteRenderer>();

        // Load dice sides sprites to array from DiceSides subfolder of Resources folder
        diceSides = Resources.LoadAll<Sprite>("Dice/DiceSides/");

        rend.sprite = diceSides[0];

        diceSelectable.transform.localScale = transform.localScale;
        diceSelectable.transform.position = transform.position;
    }

    void Update()
    {
        diceSelectable.GetComponent<SpriteRenderer>().enabled = actionable;
    }
	
    // If you left click over the dice then RollTheDice coroutine is started
    void OnMouseDown()
    {
        if (actionable)
		{
            GameManager.RollDice();
		}
    }

    // Coroutine that rolls the dice
    public IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            rend.sprite = diceSides[randomDiceSide];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        result = randomDiceSide + 1;
    }
}

public class RollEvent : Event
{
    private Dice dice1;
    private Dice dice2;

    public RollEvent(GameObject dice1, GameObject dice2)
    {
        this.dice1 = dice1.GetComponent<Dice>();
        this.dice2 = dice2.GetComponent<Dice>();
    }

    public new IEnumerator RunEvent()
    {
        yield return dice1.RollTheDice();
        yield return dice2.RollTheDice();
    }
}
