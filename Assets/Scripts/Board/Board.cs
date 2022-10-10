using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private List<GameObject> tiles;

    private const int NUM_PROPERTIES = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NUM_PROPERTIES; i++) {
            GameObject tileObj = new GameObject("test", typeof(BoxCollider), typeof(BasicTile), typeof(SpriteRenderer));
            tileObj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Board/red space");
            tileObj.transform.position = new Vector3(0f, 0f, 0f);
            tileObj.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
