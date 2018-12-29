using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherEffectController : MonoBehaviour {

	public GameObject flames;
	public GameObject smokes;
	
	void Update () 
	{

		if (PlayerStats.Multiplier > 3f)
		{
			FlameOn();
		}
		else if (PlayerStats.Multiplier > 2f)
		{
			SmokeOn();
		}
		else 
		{
			EffectsOff();
		}
	}
	
	public void FlameOn()
	{
		flames.SetActive(true);
	}

	public void EffectsOff()
	{
		flames.SetActive(false);
		smokes.SetActive(false);
	}

	public void SmokeOn()
	{
		smokes.SetActive(true);
	}
}
