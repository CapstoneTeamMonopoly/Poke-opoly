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
};