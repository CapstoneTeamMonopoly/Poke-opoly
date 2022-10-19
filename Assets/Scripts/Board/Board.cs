using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<GameObject> tiles;

    private const int NUM_TILES = 40;

    private float BOARD_WIDTH_P;
    private float BOARD_HEIGHT_P;

    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>();

        sprite = this.GetComponent<SpriteRenderer>().sprite;
        Rect boardRect = sprite.textureRect;
        BOARD_WIDTH_P = boardRect.width;
        BOARD_HEIGHT_P = boardRect.height;

        // TODO: this is temporary, it just adds tiles and gives their sprites and indexes, we need to add each tile individually, so as to customize them, and figure out how to stratify tile charicteristics
        for (int i = 0; i < NUM_TILES; i++)
        {
            GameObject tileObj = new GameObject("tile-" + i, typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
            
            switch (i)
            {
                case 0:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
                    break;
                case 1:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
                    break;
                case 2:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 3:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/brown space");
                    break;
                case 4:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 5:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 6:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
                    break;
                case 7:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 8:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
                    break;
                case 9:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/light blue space");
                    break;
                case 10:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
                    break;
                case 11:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
                    break;
                case 12:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 13:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
                    break;
                case 14:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/purple space");
                    break;
                case 15:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 16:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
                    break;
                case 17:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 18:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
                    break;
                case 19:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/orange space");
                    break;
                case 20:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
                    break;
                case 21:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
                    break;
                case 22:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 23:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
                    break;
                case 24:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
                    break;
                case 25:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 26:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
                    break;
                case 27:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
                    break;
                case 28:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 29:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/yellow space");
                    break;
                case 30:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/100");
                    break;
                case 31:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
                    break;
                case 32:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
                    break;
                case 33:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 34:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/green space");
                    break;
                case 35:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 36:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 37:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
                    break;
                case 38:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Money/500");
                    break;
                case 39:
                    tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/dark blue space");
                    break;
                default:
                    Debug.Log("We in here");
                    Debug.LogError("There shouldn't be more than 40 tiles created");
                    break;
            }
            Debug.Log(i);
            tileObj.GetComponent<BasicTile>().index = i;
            tiles.Add(tileObj);
        }
        

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

            tile.transform.localScale = new Vector3(BOARD_WIDTH_P * transform.localScale.x / (tileRect.width * 11) * unitDisplayRatio, BOARD_HEIGHT_P * transform.localScale.y / (tileRect.height * 11) * unitDisplayRatio, 1f);
            tile.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
