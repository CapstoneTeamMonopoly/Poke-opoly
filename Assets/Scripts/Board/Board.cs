using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<GameObject> tiles;
    private List<GameObject> players;
    private List<GameObject> dice;
    private const int NUM_TILES = 40;

    // Start is called before the first frame update
    void Start()
    {
        // Create board tiles
        InstantiateTiles();
        AutoplaceTiles();

        // Create players
        InstantiatePlayers();

        // Create dice
        InstantiateDice();

        // Initiate game manager
        GameManager.InitGameObjects(gameObject, ref(players), ref(tiles), ref(dice));
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
    }

    private void InstantiatePlayers()
    {
        players = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            GameObject playerObj = new GameObject("player-" + i, typeof(SpriteRenderer), typeof(Player));
            playerObj.GetComponent<Player>().id = i;
            playerObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Board/player_{i}");
            playerObj.GetComponent<Player>().MovePlayer(tiles[0]);
            players.Add(playerObj);
        }
    }

    private void InstantiateTiles()
    {
        tiles = new List<GameObject>();

        // Go!
        GameObject tileObj0 = new GameObject("tile-0", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj0);
        tiles[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[0].GetComponent<BasicTile>().index = 0;


        GameObject tileObj1 = new GameObject("tile-1", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj1);
        tiles[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
        tiles[1].GetComponent<BasicTile>().index = 1;
        tiles[1].GetComponent<PropertyTile>().PurchasePrice = 60;
        tiles[1].GetComponent<PropertyTile>().BaseLandingPrice = 2;


        GameObject tileObj2 = new GameObject("tile-2", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj2);
        tiles[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[2].GetComponent<BasicTile>().index = 2;


        GameObject tileObj3 = new GameObject("tile-3", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj3);
        tiles[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
        tiles[3].GetComponent<BasicTile>().index = 3;
        tiles[3].GetComponent<PropertyTile>().PurchasePrice = 60;
        tiles[3].GetComponent<PropertyTile>().BaseLandingPrice = 4;


        GameObject tileObj4 = new GameObject("tile-4", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj4);
        tiles[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[4].GetComponent<BasicTile>().index = 4;


        GameObject tileObj5 = new GameObject("tile-5", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj5);
        tiles[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[5].GetComponent<BasicTile>().index = 5;


        GameObject tileObj6 = new GameObject("tile-6", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj6);
        tiles[6].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[6].GetComponent<BasicTile>().index = 6;
        tiles[6].GetComponent<PropertyTile>().PurchasePrice = 100;
        tiles[6].GetComponent<PropertyTile>().BaseLandingPrice = 6;


        GameObject tileObj7 = new GameObject("tile-7", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj7);
        tiles[7].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[7].GetComponent<BasicTile>().index = 7;


        GameObject tileObj8 = new GameObject("tile-8", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj8);
        tiles[8].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[8].GetComponent<BasicTile>().index = 8;
        tiles[8].GetComponent<PropertyTile>().PurchasePrice = 100;
        tiles[8].GetComponent<PropertyTile>().BaseLandingPrice = 6;


        GameObject tileObj9 = new GameObject("tile-9", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj9);
        tiles[9].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[9].GetComponent<BasicTile>().index = 9;
        tiles[9].GetComponent<PropertyTile>().PurchasePrice = 120;
        tiles[9].GetComponent<PropertyTile>().BaseLandingPrice = 8;


        // Next Corner!
        GameObject tileObj10 = new GameObject("tile-10", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj10);
        tiles[10].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[10].GetComponent<BasicTile>().index = 10;


        GameObject tileObj11 = new GameObject("tile-11", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj11);
        tiles[11].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[11].GetComponent<BasicTile>().index = 11;
        tiles[11].GetComponent<PropertyTile>().PurchasePrice = 140;
        tiles[11].GetComponent<PropertyTile>().BaseLandingPrice = 10;


        GameObject tileObj12 = new GameObject("tile-12", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj12);
        tiles[12].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[12].GetComponent<BasicTile>().index = 12;


        GameObject tileObj13 = new GameObject("tile-13", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj13);
        tiles[13].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[13].GetComponent<BasicTile>().index = 13;
        tiles[13].GetComponent<PropertyTile>().PurchasePrice = 140;
        tiles[13].GetComponent<PropertyTile>().BaseLandingPrice = 10;


        GameObject tileObj14 = new GameObject("tile-14", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj14);
        tiles[14].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[14].GetComponent<BasicTile>().index = 14;
        tiles[14].GetComponent<PropertyTile>().PurchasePrice = 160;
        tiles[14].GetComponent<PropertyTile>().BaseLandingPrice = 12;


        GameObject tileObj15 = new GameObject("tile-15", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj15);
        tiles[15].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[15].GetComponent<BasicTile>().index = 15;


        GameObject tileObj16 = new GameObject("tile-16", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj16);
        tiles[16].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[16].GetComponent<BasicTile>().index = 16;
        tiles[16].GetComponent<PropertyTile>().PurchasePrice = 180;
        tiles[16].GetComponent<PropertyTile>().BaseLandingPrice = 14;


        GameObject tileObj17 = new GameObject("tile-17", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj17);
        tiles[17].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[17].GetComponent<BasicTile>().index = 17;


        GameObject tileObj18 = new GameObject("tile-18", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj18);
        tiles[18].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[18].GetComponent<BasicTile>().index = 18;
        tiles[18].GetComponent<PropertyTile>().PurchasePrice = 180;
        tiles[18].GetComponent<PropertyTile>().BaseLandingPrice = 14;


        GameObject tileObj19 = new GameObject("tile-19", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj19);
        tiles[19].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[19].GetComponent<BasicTile>().index = 19;
        tiles[19].GetComponent<PropertyTile>().PurchasePrice = 200;
        tiles[19].GetComponent<PropertyTile>().BaseLandingPrice = 16;


        // Next corner!
        GameObject tileObj20 = new GameObject("tile-20", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj20);
        tiles[20].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[20].GetComponent<BasicTile>().index = 20;


        GameObject tileObj21 = new GameObject("tile-21", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj21);
        tiles[21].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[21].GetComponent<BasicTile>().index = 21;
        tiles[21].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[21].GetComponent<PropertyTile>().BaseLandingPrice = 18;


        GameObject tileObj22 = new GameObject("tile-22", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj22);
        tiles[22].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[22].GetComponent<BasicTile>().index = 22;


        GameObject tileObj23 = new GameObject("tile-23", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj23);
        tiles[23].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[23].GetComponent<BasicTile>().index = 23;
        tiles[23].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[23].GetComponent<PropertyTile>().BaseLandingPrice = 18;


        GameObject tileObj24 = new GameObject("tile-24", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj24);
        tiles[24].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[24].GetComponent<BasicTile>().index = 24;
        tiles[24].GetComponent<PropertyTile>().PurchasePrice = 220;
        tiles[24].GetComponent<PropertyTile>().BaseLandingPrice = 18;


        GameObject tileObj25 = new GameObject("tile-25", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj25);
        tiles[25].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[25].GetComponent<BasicTile>().index = 25;


        GameObject tileObj26 = new GameObject("tile-26", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj26);
        tiles[26].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[26].GetComponent<BasicTile>().index = 26;
        tiles[26].GetComponent<PropertyTile>().PurchasePrice = 240;
        tiles[26].GetComponent<PropertyTile>().BaseLandingPrice = 20;


        GameObject tileObj27 = new GameObject("tile-27", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj27);
        tiles[27].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[27].GetComponent<BasicTile>().index = 27;
        tiles[27].GetComponent<PropertyTile>().PurchasePrice = 260;
        tiles[27].GetComponent<PropertyTile>().BaseLandingPrice = 22;


        GameObject tileObj28 = new GameObject("tile-28", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj28);
        tiles[28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[28].GetComponent<BasicTile>().index = 28;


        GameObject tileObj29 = new GameObject("tile-29", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj29);
        tiles[29].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[29].GetComponent<BasicTile>().index = 29;
        tiles[29].GetComponent<PropertyTile>().PurchasePrice = 280;
        tiles[29].GetComponent<PropertyTile>().BaseLandingPrice = 24;


        // Last Corner!
        GameObject tileObj30 = new GameObject("tile-30", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj30);
        tiles[30].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[30].GetComponent<BasicTile>().index = 30;


        GameObject tileObj31 = new GameObject("tile-31", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj31);
        tiles[31].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[31].GetComponent<BasicTile>().index = 31;
        tiles[31].GetComponent<PropertyTile>().PurchasePrice = 300;
        tiles[31].GetComponent<PropertyTile>().BaseLandingPrice = 26;


        GameObject tileObj32 = new GameObject("tile-32", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj32);
        tiles[32].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[32].GetComponent<BasicTile>().index = 32;
        tiles[32].GetComponent<PropertyTile>().PurchasePrice = 300;
        tiles[32].GetComponent<PropertyTile>().BaseLandingPrice = 26;


        GameObject tileObj33 = new GameObject("tile-33", typeof(BoxCollider), typeof(CommunityChestTile), typeof(SpriteRenderer));
        tiles.Add(tileObj33);
        tiles[33].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[33].GetComponent<BasicTile>().index = 33;


        GameObject tileObj34 = new GameObject("tile-34", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj34);
        tiles[34].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[34].GetComponent<BasicTile>().index = 34;
        tiles[34].GetComponent<PropertyTile>().PurchasePrice = 320;
        tiles[34].GetComponent<PropertyTile>().BaseLandingPrice = 28;


        GameObject tileObj35 = new GameObject("tile-35", typeof(BoxCollider), typeof(RailroadTile), typeof(SpriteRenderer));
        tiles.Add(tileObj35);
        tiles[35].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[35].GetComponent<BasicTile>().index = 35;


        GameObject tileObj36 = new GameObject("tile-36", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj36);
        tiles[36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[36].GetComponent<BasicTile>().index = 36;


        GameObject tileObj37 = new GameObject("tile-37", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj37);
        tiles[37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
        tiles[37].GetComponent<BasicTile>().index = 37;
        tiles[37].GetComponent<PropertyTile>().PurchasePrice = 350;
        tiles[37].GetComponent<PropertyTile>().BaseLandingPrice = 35;


        GameObject tileObj38 = new GameObject("tile-38", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
        tiles.Add(tileObj38);
        tiles[38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/base tile");
        tiles[38].GetComponent<BasicTile>().index = 38;


        GameObject tileObj39 = new GameObject("tile-39", typeof(BoxCollider), typeof(PropertyTile), typeof(SpriteRenderer));
        tiles.Add(tileObj39);
        tiles[39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
        tiles[39].GetComponent<BasicTile>().index = 39;
        tiles[39].GetComponent<PropertyTile>().PurchasePrice = 400;
        tiles[39].GetComponent<PropertyTile>().BaseLandingPrice = 50;
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
