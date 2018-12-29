using System;
using System.Collections;
using UnityEngine;

public class MainMenuTracker : MonoBehaviour
{
	public static MainMenuTracker Instance;

	public TapToStartUI tapToStartUI;
	
	public bool mainMenu;

	public DateTime LastLogin;
	public int Income;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}

		if (Instance != this)
		{
			Destroy(this);
		}
	}

	void Start()
	{
		StartCoroutine(LateStart());
	}
	
	IEnumerator LateStart()
	{
		yield return new WaitForSeconds(0.1f);
		
		if (!PlayerPrefs.HasKey("LastLogin"))
		{
			PlayerPrefs.SetString("LastLogin", DateTime.Now + "");
			PlayerPrefs.Save();
		}

		Income = 10 * (int) Mathf.Pow(1.3f, PlayerStats.IncomeLevel);
		
		//Income = PlayerStats.IncomeLevel;
		
		LastLogin = DateTime.Parse(PlayerPrefs.GetString("LastLogin"));
		TimeSpan difference = DateTime.Now - LastLogin;
		double seconds = difference.TotalSeconds;
		
		int earnedMoney = (int) (seconds * Income * 0.01f);

		if (earnedMoney > 0)
		{
			tapToStartUI.ShowPassiveIncomePanel( earnedMoney );
		}
		
		Debug.Log("Earned Money: " + earnedMoney);

		PlayerStats.Money += earnedMoney;
		PlayerPrefs.SetInt("Money", PlayerStats.Money);
		
		Debug.Log( "Now: " + DateTime.Now + " LastLogin: " + LastLogin + " diff:" + difference + "/" + " secs: " + seconds);
		
		PlayerPrefs.SetString("LastLogin", DateTime.Now + "");
		PlayerPrefs.Save();
	}
}
