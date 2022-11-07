using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	//How much money a player has at any given time.
	public int money { get; private set; }

	//Bankruptcy flag set to true if they ever go bankrupt.
	public bool bankrupt;

	//Position that changes over time.
	public int position { get; private set; }

	//Flag for if player is an AI
	public bool playerControlled;

	//Player id
	public int id;

	void Start()
    {
		bankrupt = false;
		money = 1500;
		position = 0;
		playerControlled = false;
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

		List<GameObject> waypoints = new List<GameObject>();

		int i = position;
		while (i != tile.index)
		{
			i++;
			i %= 40; // Wraps around

			if (i % 10 == 0) waypoints.Add(GameManager.GetTile(i));
		}


		waypoints.Add(dest);
		position = tile.index;

		foreach (GameObject waypoint in waypoints)
        {
			yield return MoveToPosition(waypoint.transform.position + offset);
        }
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator MoveToPosition(Vector3 dest)
    {
		while (gameObject.transform.position != dest)
        {
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, dest, 20 * Time.deltaTime);
			yield return null;
        }
    }

	public void ChangeBalance(int amount)
    {
		if (!bankrupt)
        {
			money += amount;
			// TODO: Call a GameManager function to do a routine visually adding money to player hand
		}
	}

	public void GoBankrupt()
    {
		bankrupt = true;
		money = 0;
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