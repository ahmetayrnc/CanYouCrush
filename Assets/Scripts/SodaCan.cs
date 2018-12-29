using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaCan : MonoBehaviour
{
	private ObjectPooler objectPooler;

	public GameObject splash;
	public Animator anim;
	public GameObject[] colliders;

	public bool isCrushed;

	void Start()
	{
		splash.SetActive(false);
		objectPooler = ObjectPooler.Instance;
	}

	void OnEnable()
	{
		splash.SetActive(false);
	}
	
	void Update()
	{
		if (transform.position.x >= 4f)
		{
			if (!isCrushed)
			{
				PlayerStats.ResetMultiplier();
			}
			objectPooler.DestroyObject(gameObject);
		}
	}
	
	public void GetCrushed(string direction)
	{
		PlayAnimation(direction);
		
		foreach (GameObject go in colliders)
		{
			go.SetActive(false);
		}

		isCrushed = true;
	}

	public void PlayAnimation(string direction)
	{
		anim.SetTrigger(direction);
	}
	
	//IEnumerator PlayAnimation(string direction)
	//{
	//	anim.SetTrigger(direction);
	//}

	public void ResetSodaCan()
	{
		anim.SetTrigger("Reset");
		foreach (GameObject go in colliders)
		{
			go.SetActive(true);
		}

		isCrushed = false;
	}

	public void PlaySplash()
	{
		StartCoroutine(PlaySplashCor());
	}

	IEnumerator PlaySplashCor()
	{
		splash.SetActive(true);
		yield return new WaitForSeconds(2f);
		splash.SetActive(false);
	}
	
}
