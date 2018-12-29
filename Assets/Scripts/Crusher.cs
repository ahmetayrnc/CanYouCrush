using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Crusher : MonoBehaviour
{
	public String crusherName;
	
	public bool unlocked;

	public Transform unlockedCrusher;
	public Transform lockedCrusher;

	public void Start()
	{
		if (!PlayerPrefs.HasKey(crusherName))
		{
			if (crusherName.Equals("Standart"))
			{
				PlayerPrefs.SetInt(crusherName,1);
				PlayerPrefs.Save();
			}
			else
			{
				PlayerPrefs.SetInt(crusherName,0);
				PlayerPrefs.Save();
			}
		}

		if (PlayerPrefs.GetInt(crusherName) == 1)
		{
			unlocked = true;
		}
		else if (PlayerPrefs.GetInt(crusherName) == 0)
		{
			unlocked = false;
		}

		if (unlocked)
		{
			lockedCrusher.gameObject.SetActive(false);
			unlockedCrusher.gameObject.SetActive(true);
		}
		else
		{
			lockedCrusher.gameObject.SetActive(true);
			unlockedCrusher.gameObject.SetActive(false);
		}
	}
	
	public void UnlockCrusher()
	{
		unlocked = true;
		lockedCrusher.gameObject.SetActive(false);
		unlockedCrusher.gameObject.SetActive(true);
		
		PlayerPrefs.SetInt(crusherName,1);
		PlayerPrefs.Save();
	}
}
