using System.Collections;
using System.Collections.Generic;
//using GameAnalyticsSDK.Setup;
using UnityEngine;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
	private GameManager gameManager;
	private ObjectPooler sodaCanPooler;
	private float countdown;
	
	public bool canSpawn;
	public float spawnTimeGap;
	
	void Start()
	{
		gameManager = GameManager.Instance;
		sodaCanPooler = ObjectPooler.Instance;
		canSpawn = true;
		countdown = spawnTimeGap;
	}
	
	void Update()
	{
		if (gameManager.gameIsOver)
		{
			return;
		}

		if (gameManager.gamePaused)
		{
			countdown = spawnTimeGap;
			return;
		}

		if (!canSpawn)
		{
			countdown = 0f;
			return;
		}

		countdown += Time.deltaTime;
		
		if (countdown >= spawnTimeGap)
		{
			countdown = 0f;
			sodaCanPooler.CreateObject();
		}
		
	}
}
