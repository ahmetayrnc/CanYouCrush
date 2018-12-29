using System.Collections;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
	public static BackgroundManager Instance;
	
	public Transform backgroundParent;

	void Awake()
	{
		Instance = this;
	}
	
	void Start()
	{
		StartCoroutine(LateStart());
	}

	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.01f);
		SetupBackground();
	}
	
	public void SetupBackground()
	{
		for (int i = 0; i < backgroundParent.childCount; i++)
		{
			backgroundParent.GetChild(i).gameObject.SetActive(false);
			if (i < PlayerStats.BackgroundLevel)
			{
				backgroundParent.GetChild(i).gameObject.SetActive(true);
				backgroundParent.GetChild(i).GetComponent<SpriteRenderer>().color = Camera.main.backgroundColor;
			}
		}
	}
}
