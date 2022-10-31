using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	//How much money a player has at any given time.
	int money = 1500;

	//Bankruptcy flag set to 1 if they ever go bankrupt.
	int bankrupt = 0;

	//Position that changes over time.
	int position = 0;

	//Possibly the ID's of the properties they own as quick reference,
	//don't know if this is needed or will work so it can be taken out.
	//Size 24 since that is how many properties there are that can be owned.
	//int[] properties = new int[24];

	//Name of the player
	private string Name;
};