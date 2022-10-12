using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<GameObject> tiles;

    private const int NUM_TILES = 36;

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
            tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
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

            float x = transform.position.x;
            float y = transform.position.y;

            float xOffset = 0;
            float yOffset = 0;

            switch (side)
            {
                case 0:
                    xOffset = 4.5f - relIndex;
                    yOffset = -4.5f;
                    break;
                case 1:
                    xOffset = -4.5f;
                    yOffset = -4.5f + relIndex;
                    break;
                case 2:
                    xOffset = -4.5f + relIndex;
                    yOffset = 4.5f;
                    break;
                case 3:
                    xOffset = 4.5f;
                    yOffset = 4.5f - relIndex;
                    break;
                default:
                    Debug.LogError("Tile object has index outside of expected range");
                    break;
            }

            xOffset *= transform.localScale.x / 10;
            yOffset *= transform.localScale.y / 10;

            tile.transform.localScale = new Vector3(BOARD_WIDTH_P * transform.localScale.x / (tileRect.width * 10) * unitDisplayRatio, BOARD_HEIGHT_P * transform.localScale.y / (tileRect.height * 10) * unitDisplayRatio, 1f);
            tile.transform.position = new Vector3(x + xOffset, y + yOffset, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
