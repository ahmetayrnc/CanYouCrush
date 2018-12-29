using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler Instance;

	public Transform ObjectParent;
	public GameObject sodaCanPrefab;
	
	public GameObject watermelonPrefab;
	public GameObject pineapplePrefab;
	public GameObject applePrefab;
	public GameObject pearPrefab;
	public GameObject grapePrefab;
	public GameObject orangePrefab;
	public GameObject tomatoPrefab;
	
	public List<GameObject> activeObjects;
	public List<GameObject> inactiveObjects;

	public Transform spawnPoint;
	public Transform conveyor;

	private GameObject[] objectPrefabs;
	
	//private int[] objectUnlocks;

	public int unlockRate;
	
	void Awake()
	{
		Instance = this;
	}
	
	void Start ()
	{
		objectPrefabs = new []
		{
			pineapplePrefab,
			watermelonPrefab,
			applePrefab, 
			orangePrefab,
			grapePrefab, 
			pearPrefab, 
			tomatoPrefab    
		};
		
		CreateInitObjects();
	}

	public void UnlockNextObject()
	{
		int index = (PlayerStats.Level + 1) / unlockRate;

		index--;
		
		if (index < 0)
		{
			return;
		}
		
		if (index >= objectPrefabs.Length)
		{
			return;
		}
		
		CreateInitObject(objectPrefabs[index]);
		
	}
	
	void CreateInitObjects()
	{
		CreateInitObject(sodaCanPrefab);

		for (int i = 0; i < objectPrefabs.Length; i++)
		{
			if (PlayerStats.Level >= unlockRate * (i + 1))
			{
				CreateInitObject(objectPrefabs[i]); 
			}
		}
	}

	void CreateInitObject( GameObject prefab )
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject go = Instantiate(prefab, ObjectParent);
			
			inactiveObjects.Add(go);
			
			go.SetActive(false);
		}
	}
	
	public void CreateObject()
	{
		int randIndex = Random.Range(0, inactiveObjects.Count);
		
		GameObject objectToBeCreated = inactiveObjects[randIndex];

		objectToBeCreated.transform.position = spawnPoint.position;

		objectToBeCreated.transform.SetParent(conveyor);
		
		objectToBeCreated.SetActive(true);
		
		objectToBeCreated.GetComponent<SodaCan>().ResetSodaCan();
		
		inactiveObjects.RemoveAt(randIndex);
		activeObjects.Add(objectToBeCreated);
	}

	public void DestroyObject(GameObject sodaCan, float time)
	{
		StartCoroutine(DestroyObjectCor(sodaCan, time));
	}

	public void DestroyObject(GameObject sodaCan)
	{
		StartCoroutine(DestroyObjectCor(sodaCan, 0));
	}
	
	IEnumerator DestroyObjectCor(GameObject sodaCan, float time)
	{
		yield return new WaitForSeconds(time);

		int index = -1;
		for (int i = 0; i < activeObjects.Count; i++)
		{
			if (activeObjects[i] == sodaCan)
			{
				index = i;
				break;
			}
		}

		if (index == -1)
		{
			yield break;
		}
		
		GameObject sodaCanToBeDisabled = activeObjects[index];

		sodaCanToBeDisabled.transform.SetParent(ObjectParent);
		
		activeObjects.RemoveAt(index);
		inactiveObjects.Add(sodaCanToBeDisabled);
		
		sodaCanToBeDisabled.SetActive(false);
	}
}
