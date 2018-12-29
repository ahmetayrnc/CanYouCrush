using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI bestText;
	public TextMeshProUGUI percentText;
	
	public Transform unlockParent;
	public Transform unlockImages;
	
	public string gameScene = "GameScene";

	void Start()
	{
		bestText.gameObject.SetActive(false);
		StartCoroutine(LateStart());
	}
    
	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		UpdateUnlockImage();
	}
	
	void Update()
	{
		scoreText.text = PlayerStats.Score + "";
		bestText.text = "New Best: " + "\n" + PlayerStats.Best + "";
		
		percentText.text = (int)((float)PlayerStats.SodaCanCount / (GameManager.Instance.levelMilestone + (int)Mathf.Pow(Mathf.Log(PlayerStats.Level),2)) * 100) + "%" +" COMPLETED";
	}

	public void ShowBest()
	{
		bestText.gameObject.SetActive(true);
	}

	public void RestartGame()
	{
		GameManager.Instance.RestartGame();
	}

	public void MainMenu()
	{
		MainMenuTracker.Instance.mainMenu = true;
		GameManager.Instance.RestartGame();
	}
	
	public void UpdateUnlockImage()
	{
		int _index = PlayerStats.Level / ObjectPooler.Instance.unlockRate;

		if (_index < 0)
		{
			return;
		}

		if (_index >= unlockImages.childCount)
		{
			unlockParent.gameObject.SetActive(false);
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
}
