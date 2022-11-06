using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	//How much money a player has at any given time.
	public int money;

	//Bankruptcy flag set to true if they ever go bankrupt.
	public bool bankrupt;

	//Position that changes over time.
	public int position;

	//Flag for if player is an AI
	public bool playerControlled;

	//Player id
	public int id;

	void Start()
    {
		bankrupt = false;
		money = 1500;
		position = 0;
		playerControlled = true;
    }

	public void DestroyPlayer()
    {
		Destroy(GetComponent<SpriteRenderer>());
    }

	public void InstantiatePlayerPosition(GameObject tile)
    {
		Vector3 offset;
		Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
		Sprite tileSprite = tile.GetComponent<SpriteRenderer>().sprite;
		Rect playerRect = sprite.textureRect;
		Rect tileRect = tileSprite.textureRect;
		float unitDisplayRatio = sprite.pixelsPerUnit / tileSprite.pixelsPerUnit;
		float offX = playerRect.width / 1.5f / sprite.pixelsPerUnit;
		float offY = playerRect.height / 1.8f / sprite.pixelsPerUnit;
		switch (id)
		{
			case 0:
				offset = new Vector3(offX, offY, -1);
				break;
			case 1:
				offset = new Vector3(-offX, offY, -1);
				break;
			case 2:
				offset = new Vector3(-offX, -offY, -1);
				break;
			case 3:
				offset = new Vector3(offX, -offY, -1);
				break;
			default:
				offset = new Vector3(0, 0, -1);
				break;
		}
		transform.localScale = new Vector3(tileRect.width * tile.transform.localScale.x / (playerRect.width * 3) * unitDisplayRatio, tileRect.height * tile.transform.localScale.y / (playerRect.height * 3) * unitDisplayRatio, 1f);


		transform.position = tile.transform.position + offset;
	}

	public IEnumerator MovePlayer(GameObject dest)
    {
		Vector3 offset;
		BasicTile tile = dest.GetComponent<BasicTile>();
		Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
		Sprite tileSprite = dest.GetComponent<SpriteRenderer>().sprite;
		Rect playerRect = sprite.textureRect;
		Rect tileRect = tileSprite.textureRect;
		float unitDisplayRatio = sprite.pixelsPerUnit / tileSprite.pixelsPerUnit;
		float offX = playerRect.width / 1.5f / sprite.pixelsPerUnit;
		float offY = playerRect.height / 1.8f / sprite.pixelsPerUnit;
		switch (id)
        {
			case 0:
				offset = new Vector3(offX, offY, -1);
				break;
			case 1:
				offset = new Vector3(-offX, offY, -1);
				break;
			case 2:
				offset = new Vector3(-offX, -offY, -1);
				break;
			case 3:
				offset = new Vector3(offX, -offY, -1);
				break;
			default:
				offset = new Vector3(0, 0, -1);
				break;
        }

		transform.position = dest.transform.position + offset;
		position = tile.index;
		yield return null;
    }
};

public class MoveEvent : Event
{
	private Player player;
	private GameObject dest;

	public MoveEvent(GameObject player, GameObject dest)
	{
		this.player = player.GetComponent<Player>();
		this.dest = dest;
	}

	public new IEnumerator RunEvent()
	{
		yield return player.MovePlayer(dest);
	}
}