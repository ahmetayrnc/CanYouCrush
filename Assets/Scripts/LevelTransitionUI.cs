using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTransitionUI : MonoBehaviour
{
	public TextMeshProUGUI level;

	public Transform unlockImagesParent;
	public Transform unlockImages;

	public Transform crusherUnlockImages;
	public Transform crusherUnlockImagesParent;
	
	void Update()
	{
		level.text = "You Passed Level " + (PlayerStats.Level - 1);
	}

	public void ShowCrusherUnlock()
	{
		unlockImagesParent.gameObject.SetActive(false);
		crusherUnlockImagesParent.gameObject.SetActive(true);

		int index = PlayerStats.Level / 11;
		index--;
		
		Debug.Log("index: "+ index);
		print("childcount" + crusherUnlockImages.childCount);
		
		if (index < 0)
		{
			return;
		}
		
		if (index >= crusherUnlockImages.childCount)
		{
			crusherUnlockImagesParent.gameObject.SetActive(false);
		}
		
		for (int i = 0; i < crusherUnlockImages.childCount; i++)
		{
			crusherUnlockImages.GetChild(i).gameObject.SetActive(false);
			if (i == index)
			{
				crusherUnlockImages.GetChild(i).gameObject.SetActive(true);
			}
		}
	}
	
	public void ShowUnlockObject()
	{
		crusherUnlockImagesParent.gameObject.SetActive(false);
		unlockImagesParent.gameObject.SetActive(true);
		
		int _index = PlayerStats.Level / ObjectPooler.Instance.unlockRate;

		if (_index < 0)
		{
			return;
		}

		if (_index >= unlockImages.childCount)
		{
			unlockImagesParent.gameObject.SetActive(false);
		}
		
		for (int i = 0; i < unlockImages.childCount; i++)
		{
			unlockImages.GetChild(i).gameObject.SetActive(false);
			if (i == _index)
			{
				unlockImages.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	public void HideUnlockObject()
	{
		unlockImagesParent.gameObject.SetActive(false);
		crusherUnlockImagesParent.gameObject.SetActive(false);
	}
}
