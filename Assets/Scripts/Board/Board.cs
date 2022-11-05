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
        GameManager.InitGameObjects(players, tiles, dice);
    }

    private void InstantiateDice()
    {
        dice = new List<GameObject>();

        GameObject dice1 = new GameObject("dice-0", typeof(SpriteRenderer), typeof(Dice));
        GameObject dice2 = new GameObject("dice-1", typeof(SpriteRenderer), typeof(Dice));
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

        for (int i = 0; i < NUM_TILES; i++)
        {
            GameObject tileObj = new GameObject("tile-" + i, typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
            tileObj.GetComponent<BasicTile>().index = i;
            tiles.Add(tileObj);
        }

        tiles[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
        tiles[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
        tiles[2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
        tiles[4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[5].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[6].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[7].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[8].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[9].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
        tiles[10].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
        tiles[11].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[12].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[13].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[14].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
        tiles[15].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[16].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[17].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[18].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[19].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
        tiles[20].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
        tiles[21].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[22].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[23].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[24].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
        tiles[25].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[26].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[27].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[28].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[29].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
        tiles[30].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
        tiles[31].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[32].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[33].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[34].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
        tiles[35].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[36].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[37].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
        tiles[38].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
        tiles[39].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
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
