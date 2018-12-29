using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherUnlock : MonoBehaviour
{
	public LevelTransitionUI levelTransitionUI;
	
	public static CrusherUnlock Instance;
	
	private Transform crushersParent;

	public int unlockRate;
	
	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		crushersParent = transform;
	}

	public void CheckUnlock()
	{
		int index = PlayerStats.Level / 11;
			
		for (int i = 0; i < crushersParent.childCount; i++)
		{
			Crusher cr = crushersParent.GetChild(i).GetComponent<Crusher>();
			if (i <= index)
			{
				cr.UnlockCrusher();
			}
		}
		
		if (PlayerStats.Level % 11 == 0)
		{
			levelTransitionUI.ShowCrusherUnlock();
		}
	}
}
