using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CrusherHeadCollision : MonoBehaviour
{
	private bool isColliding;
	private ScoreUI ui;
	
	[HideInInspector]
	public bool hasCrushed;
	public BoxCollider col;
	
	void Start()
	{
		ui = ScoreUI.Instance;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (isColliding)
		{
			return;
		}
		
		if (other.CompareTag("SodaCan_Middle"))
		{
			isColliding = true;
			PerfectHit(other);
		}
		
		else if (other.CompareTag("SodaCan_Left"))
		{
			isColliding = true;
			NotSoPerfectHit(other, "Left");
		}
		
		else if (other.CompareTag("SodaCan_Right"))
		{
			isColliding = true;
			NotSoPerfectHit(other, "Right");
		}
	}

	void Update()
	{
		isColliding = false;
	}
	
	void PerfectHit(Collider other)
	{
		iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
		
		PlayerStats.IncrementSodaCanCount();
		PlayerStats.IncrementMultiplier();
		PlayerStats.IncrementScore();
	
		ui.CreateMotivationPopUp(PlayerStats.Multiplier);
		ui.CreateScorePopUp(PlayerStats.Multiplier * PlayerStats.Level);
		ui.CreateMoneyPopUp();
		
		SodaCan sodaCan = other.GetComponentInParent<SodaCan>();
		sodaCan.GetCrushed("Middle");
		sodaCan.PlaySplash();
		ui.CreateSplashEffect( sodaCan.splash.GetComponent<ParticleSystem>().main.startColor.color );
		
		hasCrushed = true;
	}
	
	void NotSoPerfectHit(Collider other, string direction)
	{	
		PlayerStats.ResetMultiplier();
		PlayerStats.IncrementScore();
		PlayerStats.IncrementSodaCanCount();	
		
		ui.CreateScorePopUp(PlayerStats.Multiplier * PlayerStats.Level);
		
		SodaCan sodaCan = other.GetComponentInParent<SodaCan>();
		sodaCan.GetCrushed(direction);
		hasCrushed = true;
	}
}
