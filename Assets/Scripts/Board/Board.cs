using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<GameObject> tiles;
    private List<GameObject> players;
    private List<GameObject> dice;
    private List<GameObject> moneyTiles;
    private GameObject communityDeck;
    private GameObject chanceDeck;
    private const int NUM_TILES = 40;

    void Start()
    {
        // Create board tiles
        InstantiateTiles();
        AutoplaceTiles();

        // Create player stats
        InstantiateMoney();

        // Create players
        InstantiatePlayers();

        // Create dice
        InstantiateDice();

        // Create Decks
        InstantiateDecks();

        // Initiate game manager
        GameManager.InitGameObjects(gameObject, ref (players), ref (tiles), ref (dice), ref (communityDeck), ref (chanceDeck));
    }

    private void InstantiateDice()
    {
        dice = new List<GameObject>();

        GameObject dice1 = new GameObject("dice-0", typeof(BoxCollider), typeof(SpriteRenderer), typeof(Dice));
        GameObject dice2 = new GameObject("dice-1", typeof(BoxCollider), typeof(SpriteRenderer), typeof(Dice));
        dice1.transform.position = new Vector3(9f, 2f, 0);
        dice2.transform.position = new Vector3(9f, -2f, 0);
        dice.Add(dice1);
        dice.Add(dice2);
        foreach (GameObject die in dice)
        {
            Vector3 size = Resources.Load<Sprite>("Dice/diceSelectable").bounds.size;
            die.GetComponent<BoxCollider>().size = size;
            die.GetComponent<Dice>().actionable = true;
        }
    }

    private void InstantiateDecks()
    {
        communityDeck = new GameObject("deck-0", typeof(BoxCollider), typeof(SpriteRenderer), typeof(CommunityDeck));
        chanceDeck = new GameObject("deck-1", typeof(BoxCollider), typeof(SpriteRenderer), typeof(ChanceDeck));
        communityDeck.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-communitychest");
        chanceDeck.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-chance");
        communityDeck.transform.position = new Vector3(-3f, -3f, -1f);
        chanceDeck.transform.position = new Vector3(3f, 3f, -1f);

    }

    private void InstantiatePlayers()
    {
        players = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject playerObj = new GameObject("player-" + i, typeof(SpriteRenderer), typeof(Player));
            playerObj.GetComponent<Player>().id = i;
            playerObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Board/player_{i}");
            playerObj.GetComponent<Player>().InstantiatePlayerPosition(tiles[0]);
            players.Add(playerObj);
            // TODO: Read player controlled values from previous scene
            playerObj.GetComponent<Player>().playerControlled = false;
            Debug.Log($"Player {i} controlled by player: {playerObj.GetComponent<Player>().playerControlled}");
        }
    }

    private void InstantiateTiles()
    {
        tiles = new List<GameObject>();

        // Go!
        GameObject tileObj0 = new GameObject("tile-0", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj0);
        tiles[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-go");
        tiles[0].GetComponent<BasicTile>().index = 0;


        GameObject tileObj1 = new GameObject("tile-1", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj1);
        tiles[1].GetComponent<BasicTile>().index = 1;
        tiles[1].GetComponent<PropertyTile>().PurchasePrice = 60;
        tiles[1].GetComponent<PropertyTile>().BaseLandingPrice = 2;
        tiles[1].GetComponent<PropertyTile>().Type = "Brown";
        tiles[1].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/brown1");


        GameObject tileObj2 = new GameObject("tile-2", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj2);
        tiles[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-communitychest");
        tiles[2].GetComponent<BasicTile>().index = 2;


        GameObject tileObj3 = new GameObject("tile-3", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj3);
        tiles[3].GetComponent<BasicTile>().index = 3;
        tiles[3].GetComponent<PropertyTile>().PurchasePrice = 60;
        tiles[3].GetComponent<PropertyTile>().BaseLandingPrice = 4;
        tiles[3].GetComponent<PropertyTile>().Type = "Brown";
        tiles[3].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/brown2");


        GameObject tileObj4 = new GameObject("tile-4", typeof(BoxCollider), typeof(TaxTile), typeof(SpriteRenderer));
        tiles.Add(tileObj4);
        tiles[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile"); // Replace this later
        tiles[4].GetComponent<BasicTile>().index = 4;
        tiles[4].GetComponent<TaxTile>().TaxAmount = 200;


        GameObject tileObj5 = new GameObject("tile-5", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj5);
        tiles[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-rr1");
        tiles[5].GetComponent<BasicTile>().index = 5;


        GameObject tileObj6 = new GameObject("tile-6", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj6);
        tiles[6].GetComponent<BasicTile>().index = 6;
        tiles[6].GetComponent<PropertyTile>().PurchasePrice = 100;
        tiles[6].GetComponent<PropertyTile>().BaseLandingPrice = 6;
        tiles[6].GetComponent<PropertyTile>().Type = "Cyan";
        tiles[6].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/cyan1");


        GameObject tileObj7 = new GameObject("tile-7", typeof(BoxCollider), typeof(ChanceTile), typeof(SpriteRenderer));
        tiles.Add(tileObj7);
        tiles[7].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-chance");
        tiles[7].GetComponent<BasicTile>().index = 7;


        GameObject tileObj8 = new GameObject("tile-8", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj8);
        tiles[8].GetComponent<BasicTile>().index = 8;
        tiles[8].GetComponent<PropertyTile>().PurchasePrice = 100;
        tiles[8].GetComponent<PropertyTile>().BaseLandingPrice = 6;
        tiles[8].GetComponent<PropertyTile>().Type = "Cyan";
        tiles[8].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/cyan2");


        GameObject tileObj9 = new GameObject("tile-9", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj9);
        tiles[9].GetComponent<BasicTile>().index = 9;
        tiles[9].GetComponent<PropertyTile>().PurchasePrice = 120;
        tiles[9].GetComponent<PropertyTile>().BaseLandingPrice = 8;
        tiles[9].GetComponent<PropertyTile>().Type = "Cyan";
        tiles[9].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/cyan3");


        // Next Corner!
        GameObject tileObj10 = new GameObject("tile-10", typeof(BoxCollider), typeof(PokemonCenterTile), typeof(SpriteRenderer));
        tiles.Add(tileObj10);
        tiles[10].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-pc");
        tiles[10].GetComponent<BasicTile>().index = 10;


        GameObject tileObj11 = new GameObject("tile-11", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj11);
        tiles[11].GetComponent<BasicTile>().index = 11;
        tiles[11].GetComponent<PropertyTile>().PurchasePrice = 140;
        tiles[11].GetComponent<PropertyTile>().BaseLandingPrice = 10;
        tiles[11].GetComponent<PropertyTile>().Type = "Purple";
        tiles[11].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/purple1");


        GameObject tileObj12 = new GameObject("tile-12", typeof(BoxCollider), typeof(UtilityTile), typeof(SpriteRenderer));
        tiles.Add(tileObj12);
        tiles[12].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-util-electric");
        tiles[12].GetComponent<BasicTile>().index = 12;
        tiles[12].GetComponent<UtilityTile>().PurchasePrice = 150;


        GameObject tileObj13 = new GameObject("tile-13", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj13);
        tiles[13].GetComponent<BasicTile>().index = 13;
        tiles[13].GetComponent<PropertyTile>().PurchasePrice = 140;
        tiles[13].GetComponent<PropertyTile>().BaseLandingPrice = 10;
        tiles[13].GetComponent<PropertyTile>().Type = "Purple";
        tiles[13].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/purple2");


        GameObject tileObj14 = new GameObject("tile-14", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj14);
        tiles[14].GetComponent<BasicTile>().index = 14;
        tiles[14].GetComponent<PropertyTile>().PurchasePrice = 160;
        tiles[14].GetComponent<PropertyTile>().BaseLandingPrice = 12;
        tiles[14].GetComponent<PropertyTile>().Type = "Purple";
        tiles[14].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/purple3");


        GameObject tileObj15 = new GameObject("tile-15", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj15);
        tiles[15].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-rr2");
        tiles[15].GetComponent<BasicTile>().index = 15;


        GameObject tileObj16 = new GameObject("tile-16", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj16);
        tiles[16].GetComponent<BasicTile>().index = 16;
        tiles[16].GetComponent<PropertyTile>().PurchasePrice = 180;
        tiles[16].GetComponent<PropertyTile>().BaseLandingPrice = 14;
        tiles[16].GetComponent<PropertyTile>().Type = "Orange";
        tiles[16].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/orange1");


        GameObject tileObj17 = new GameObject("tile-17", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj17);
        tiles[17].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-communitychest");
        tiles[17].GetComponent<BasicTile>().index = 17;


        GameObject tileObj18 = new GameObject("tile-18", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj18);
        tiles[18].GetComponent<BasicTile>().index = 18;
        tiles[18].GetComponent<PropertyTile>().PurchasePrice = 180;
        tiles[18].GetComponent<PropertyTile>().BaseLandingPrice = 14;
        tiles[18].GetComponent<PropertyTile>().Type = "Orange";
        tiles[18].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/orange2");


        GameObject tileObj19 = new GameObject("tile-19", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj19);
        tiles[19].GetComponent<BasicTile>().index = 19;
        tiles[19].GetComponent<PropertyTile>().PurchasePrice = 200;
        tiles[19].GetComponent<PropertyTile>().BaseLandingPrice = 16;
        tiles[19].GetComponent<PropertyTile>().Type = "Orange";
        tiles[19].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/orange3");


        // Next corner!
        GameObject tileObj20 = new GameObject("tile-20", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj20);
        tiles[20].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile"); // Change this later
        tiles[20].GetComponent<BasicTile>().index = 20;


        GameObject tileObj21 = new GameObject("tile-21", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj21);
        tiles[21].GetComponent<BasicTile>().index = 21;
        tiles[21].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[21].GetComponent<PropertyTile>().BaseLandingPrice = 18;
        tiles[21].GetComponent<PropertyTile>().Type = "Red";
        tiles[21].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/red1");


        GameObject tileObj22 = new GameObject("tile-22", typeof(BoxCollider), typeof(ChanceTile), typeof(SpriteRenderer));
        tiles.Add(tileObj22);
        tiles[22].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-chance");
        tiles[22].GetComponent<BasicTile>().index = 22;


        GameObject tileObj23 = new GameObject("tile-23", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj23);
        tiles[23].GetComponent<BasicTile>().index = 23;
        tiles[23].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[23].GetComponent<PropertyTile>().BaseLandingPrice = 18;
        tiles[23].GetComponent<PropertyTile>().Type = "Red";
        tiles[23].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/red2");


        GameObject tileObj24 = new GameObject("tile-24", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj24);
        tiles[24].GetComponent<BasicTile>().index = 24;
        tiles[24].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[24].GetComponent<PropertyTile>().BaseLandingPrice = 18;
        tiles[24].GetComponent<PropertyTile>().Type = "Red";
        tiles[24].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/red3");


        GameObject tileObj25 = new GameObject("tile-25", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj25);
        tiles[25].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-rr3");
        tiles[25].GetComponent<BasicTile>().index = 25;


        GameObject tileObj26 = new GameObject("tile-26", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj26);
        tiles[26].GetComponent<BasicTile>().index = 26;
        tiles[26].GetComponent<PropertyTile>().PurchasePrice = 240;
        tiles[26].GetComponent<PropertyTile>().BaseLandingPrice = 20;
        tiles[26].GetComponent<PropertyTile>().Type = "Yellow";
        tiles[26].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/yellow1");


        GameObject tileObj27 = new GameObject("tile-27", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj27);
        tiles[27].GetComponent<BasicTile>().index = 27;
        tiles[27].GetComponent<PropertyTile>().PurchasePrice = 260;
        tiles[27].GetComponent<PropertyTile>().BaseLandingPrice = 22;
        tiles[27].GetComponent<PropertyTile>().Type = "Yellow";
        tiles[27].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/yellow2");


        GameObject tileObj28 = new GameObject("tile-28", typeof(BoxCollider), typeof(UtilityTile), typeof(SpriteRenderer));
        tiles.Add(tileObj28);
        tiles[28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-util-water");
        tiles[28].GetComponent<BasicTile>().index = 28;
        tiles[28].GetComponent<UtilityTile>().PurchasePrice = 150;


        GameObject tileObj29 = new GameObject("tile-29", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj29);
        tiles[29].GetComponent<BasicTile>().index = 29;
        tiles[29].GetComponent<PropertyTile>().PurchasePrice = 280;
        tiles[29].GetComponent<PropertyTile>().BaseLandingPrice = 24;
        tiles[29].GetComponent<PropertyTile>().Type = "Yellow";
        tiles[29].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/yellow3");


        // Last Corner!
        GameObject tileObj30 = new GameObject("tile-30", typeof(BoxCollider), typeof(TeamRocketTile), typeof(SpriteRenderer));
        tiles.Add(tileObj30);
        tiles[30].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-teamrocket");
        tiles[30].GetComponent<BasicTile>().index = 30;


        GameObject tileObj31 = new GameObject("tile-31", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj31);
        tiles[31].GetComponent<BasicTile>().index = 31;
        tiles[31].GetComponent<PropertyTile>().PurchasePrice = 300;
        tiles[31].GetComponent<PropertyTile>().BaseLandingPrice = 26;
        tiles[31].GetComponent<PropertyTile>().Type = "Green";
        tiles[31].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/green1");


        GameObject tileObj32 = new GameObject("tile-32", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj32);
        tiles[32].GetComponent<BasicTile>().index = 32;
        tiles[32].GetComponent<PropertyTile>().PurchasePrice = 300;
        tiles[32].GetComponent<PropertyTile>().BaseLandingPrice = 26;
        tiles[32].GetComponent<PropertyTile>().Type = "Green";
        tiles[32].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/green2");


        GameObject tileObj33 = new GameObject("tile-33", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj33);
        tiles[33].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-communitychest");
        tiles[33].GetComponent<BasicTile>().index = 33;


        GameObject tileObj34 = new GameObject("tile-34", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj34);
        tiles[34].GetComponent<BasicTile>().index = 34;
        tiles[34].GetComponent<PropertyTile>().PurchasePrice = 320;
        tiles[34].GetComponent<PropertyTile>().BaseLandingPrice = 28;
        tiles[34].GetComponent<PropertyTile>().Type = "Green";
        tiles[34].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/green3");


        GameObject tileObj35 = new GameObject("tile-35", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj35);
        tiles[35].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-rr4");
        tiles[35].GetComponent<BasicTile>().index = 35;


        GameObject tileObj36 = new GameObject("tile-36", typeof(BoxCollider), typeof(ChanceTile), typeof(SpriteRenderer));
        tiles.Add(tileObj36);
        tiles[36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/PokemonTiles/tile-chance");
        tiles[36].GetComponent<BasicTile>().index = 36;


        GameObject tileObj37 = new GameObject("tile-37", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj37);
        tiles[37].GetComponent<BasicTile>().index = 37;
        tiles[37].GetComponent<PropertyTile>().PurchasePrice = 350;
        tiles[37].GetComponent<PropertyTile>().BaseLandingPrice = 35;
        tiles[37].GetComponent<PropertyTile>().Type = "Blue";
        tiles[37].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/blue1");


        GameObject tileObj38 = new GameObject("tile-38", typeof(BoxCollider), typeof(TaxTile), typeof(SpriteRenderer));
        tiles.Add(tileObj38);
        tiles[38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile"); // Change this later
        tiles[38].GetComponent<BasicTile>().index = 38;
        tiles[38].GetComponent<TaxTile>().TaxAmount = 100;


        GameObject tileObj39 = new GameObject("tile-39", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj39);
        tiles[39].GetComponent<BasicTile>().index = 39;
        tiles[39].GetComponent<PropertyTile>().PurchasePrice = 400;
        tiles[39].GetComponent<PropertyTile>().BaseLandingPrice = 50;
        tiles[39].GetComponent<PropertyTile>().Type = "Blue";
        tiles[39].GetComponent<PropertyTile>().SetBaseSprite("Board/PokemonTiles/blue2");
    }

    private void InstantiateMoney()
    {

        moneyTiles = new List<GameObject>();

        GameObject p0_1 = new GameObject("p0_1", typeof(SpriteRenderer));
        moneyTiles.Add(p0_1);
        moneyTiles[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/1");
        moneyTiles[0].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[0].transform.position = new Vector3(-9.4f, -0.6f, 0f);

        GameObject p0_2 = new GameObject("p0_2", typeof(SpriteRenderer));
        moneyTiles.Add(p0_2);
        moneyTiles[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/5");
        moneyTiles[1].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[1].transform.position = new Vector3(-9.1f, -0.6f, 0f);

        GameObject p0_3 = new GameObject("p0_3", typeof(SpriteRenderer));
        moneyTiles.Add(p0_3);
        moneyTiles[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[2].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[2].transform.position = new Vector3(-8.8f, -0.6f, 0f);

        GameObject p0_4 = new GameObject("p0_4", typeof(SpriteRenderer));
        moneyTiles.Add(p0_4);
        moneyTiles[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[3].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[3].transform.position = new Vector3(-8.5f, -0.6f, 0f);

        GameObject p1_1 = new GameObject("p1_1", typeof(SpriteRenderer));
        moneyTiles.Add(p1_1);
        moneyTiles[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/1");
        moneyTiles[4].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[4].transform.position = new Vector3(-9.5f, -2.4f, 0f);

        GameObject p1_2 = new GameObject("p1_2", typeof(SpriteRenderer));
        moneyTiles.Add(p1_2);
        moneyTiles[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/5");
        moneyTiles[5].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[5].transform.position = new Vector3(-9.2f, -2.4f, 0f);

        GameObject p1_3 = new GameObject("p1_3", typeof(SpriteRenderer));
        moneyTiles.Add(p1_3);
        moneyTiles[6].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[6].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[6].transform.position = new Vector3(-8.9f, -2.4f, 0f);

        GameObject p1_4 = new GameObject("p1_4", typeof(SpriteRenderer));
        moneyTiles.Add(p1_4);
        moneyTiles[7].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[7].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[7].transform.position = new Vector3(-8.6f, -2.4f, 0f);

        GameObject p2_1 = new GameObject("p2_1", typeof(SpriteRenderer));
        moneyTiles.Add(p2_1);
        moneyTiles[8].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/1");
        moneyTiles[8].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[8].transform.position = new Vector3(-9.5f, -4.1f, 0f);

        GameObject p2_2 = new GameObject("p2_2", typeof(SpriteRenderer));
        moneyTiles.Add(p2_2);
        moneyTiles[9].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/5");
        moneyTiles[9].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[9].transform.position = new Vector3(-9.2f, -4.1f, 0f);

        GameObject p2_3 = new GameObject("p2_3", typeof(SpriteRenderer));
        moneyTiles.Add(p2_3);
        moneyTiles[10].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[10].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[10].transform.position = new Vector3(-8.9f, -4.1f, 0f);

        GameObject p2_4 = new GameObject("p2_4", typeof(SpriteRenderer));
        moneyTiles.Add(p2_4);
        moneyTiles[11].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[11].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[11].transform.position = new Vector3(-8.6f, -4.1f, 0f);

        GameObject p3_1 = new GameObject("p3_1", typeof(SpriteRenderer));
        moneyTiles.Add(p3_1);
        moneyTiles[12].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/1");
        moneyTiles[12].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[12].transform.position = new Vector3(-9.4f, -5.95f, 0f);

        GameObject p3_2 = new GameObject("p3_2", typeof(SpriteRenderer));
        moneyTiles.Add(p3_2);
        moneyTiles[13].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/5");
        moneyTiles[13].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[13].transform.position = new Vector3(-9.1f, -5.95f, 0f);

        GameObject p3_3 = new GameObject("p3_3", typeof(SpriteRenderer));
        moneyTiles.Add(p3_3);
        moneyTiles[14].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[14].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[14].transform.position = new Vector3(-8.8f, -5.95f, 0f);

        GameObject p3_4 = new GameObject("p3_4", typeof(SpriteRenderer));
        moneyTiles.Add(p3_4);
        moneyTiles[15].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("DigitSprites/0");
        moneyTiles[15].transform.localScale = new Vector3(.5f, .5f, 0f);
        moneyTiles[15].transform.position = new Vector3(-8.5f, -5.95f, 0f);

    }

    public IEnumerator UpdateMoney(Player p, int src, int dest)
    {
        Debug.Log("called");
        List<SpriteRenderer> digits = new List<SpriteRenderer>();
        int bRange = 0;
        int tRange = 0;
        switch (p.id)
        {
            case 0:
                bRange = 0;
                tRange = 3;
                break;
            case 1:
                bRange = 4;
                tRange = 7;
                break;
            case 2:
                bRange = 8;
                tRange = 11;
                break;
            case 3:
                bRange = 12;
                tRange = 15;
                break;
        }

        for (int i = bRange; i <= tRange; i++)
        {
            digits.Add(moneyTiles[i].GetComponent<SpriteRenderer>());
        }

        int iters = 60;
        for (int i = 0; i < iters; i++)
        {

            float easer = (float)i/iters;
            int easedMoney = src + (int)((dest - src) * easer);
            yield return SetMoney(easedMoney, digits);
            yield return new WaitForSeconds(0.6f/iters);
        }
        yield return SetMoney(dest, digits);
    }

    private IEnumerator SetMoney(int money, List<SpriteRenderer> moneyDigits)
    {
        int pThousands = money / 1000; // 1500 / 1000 = 1
        int pHundreds = (money / 100) % 10; // 1500 / 100 = 15 % 10 = 5
        int pTens = (money / 10) % 10; // 1500 / 10 = 150 % 10 = 0 
        int pOnes = money % 10; // 1500 % 10 = 150 with remainder 0 = 0

        moneyDigits[0].sprite = Resources.Load<Sprite>($"DigitSprites/{pThousands}");
        moneyDigits[1].sprite = Resources.Load<Sprite>($"DigitSprites/{pHundreds}");
        moneyDigits[2].sprite = Resources.Load<Sprite>($"DigitSprites/{pTens}");
        moneyDigits[3].sprite = Resources.Load<Sprite>($"DigitSprites/{pOnes}");
        yield return null;
    }

    private void AutoplaceTiles()
    {
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        Rect boardRect = sprite.textureRect;
        float boardWidthPx = boardRect.width;
        float boardHeightPx = boardRect.height;

        /*
         *  This foreach loop places each tile around the board in the respective place for all tiles with indexes 0-35, starting at the bottom right and working clockwise
         */ 
        foreach (GameObject tile in tiles)
        {
            BasicTile tileScript = tile.GetComponent<BasicTile>();
            Sprite tileSprite = tile.GetComponent<SpriteRenderer>().sprite;
            Rect tileRect = tileSprite.textureRect;

            float unitDisplayRatio = tileSprite.pixelsPerUnit / sprite.pixelsPerUnit;

            int side = tileScript.index / (NUM_TILES / 4);
            int relIndex = tileScript.index % (NUM_TILES / 4);

            float xOffset = 0;
            float yOffset = 0;

            switch (side)
            {
                case 0:
                    xOffset = 5f - relIndex;
                    yOffset = -5f;
                    break;
                case 1:
                    xOffset = -5f;
                    yOffset = -5f + relIndex;
                    break;
                case 2:
                    xOffset = -5f + relIndex;
                    yOffset = 5f;
                    break;
                case 3:
                    xOffset = 5f;
                    yOffset = 5f - relIndex;
                    break;
                default:
                    Debug.LogError("Tile object has index outside of expected range");
                    break;
            }

            xOffset *= transform.localScale.x / 11;
            yOffset *= transform.localScale.y / 11;

            tile.transform.localScale = new Vector3(boardWidthPx * transform.localScale.x / (tileRect.width * 11) * unitDisplayRatio, boardHeightPx * transform.localScale.y / (tileRect.height * 11) * unitDisplayRatio, 1f);
            tile.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0f);
        }
    }
}

public class MoneyEvent : Event
{
    Player player;
    Board board;
    int src;
    int dest;

    public MoneyEvent(Player p, Board b, int src, int dest)
    {
        this.player = p;
        this.board = b;
        this.src = src;
        this.dest = dest;
    }

    public IEnumerator RunEvent()
    {
        yield return board.UpdateMoney(player, src, dest);
    }
}
