using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using EZCameraShake;

using DG.Tweening;

public class CrusherMovement : MonoBehaviour
{
	public CrusherHeadCollision collision;
	public GameObject head;
	public Transform endPoint;
	public Transform startPoint;
	public float crushDuration;
	public float missShakeRoughness;
	public float missShakeMagnitude;
	
	private bool isCrushing;
	private GameManager gameManager;
	
	void Start ()
	{
		isCrushing = false;
		gameManager = GameManager.Instance;
	}
	
	void Update () 
	{
		if (gameManager.gameIsOver)
		{
			return;
		}
		
		if (gameManager.gamePaused)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown(0) && !isCrushing)
		{
			Crush();
		}
	}

	void Crush()
	{
		isCrushing = true;
		collision.hasCrushed = false;
		collision.col.enabled = true;

		float tweenTime = crushDuration / 2;
		
		head.transform.DOMoveY(endPoint.position.y, tweenTime).OnComplete(() =>
		{
			if (collision.hasCrushed == false) 
			{
				Missed();
			}
				
			collision.col.enabled = false;
				
			head.transform.DOMoveY(startPoint.position.y, tweenTime).OnComplete(() =>
			{
				isCrushing = false;
					
			}).SetEase(Ease.Linear);
				
		}).SetEase(Ease.Linear);
	}
	
	void Missed()
	{
		CameraShaker.Instance.ShakeOnce(missShakeMagnitude, missShakeRoughness, 0.1f, 1.2f);
		EndGame();
	}
	
	void EndGame()
	{
		GameManager.Instance.EndGame();
	}
}
