using System.Collections;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	public static ScoreUI Instance;

	public Transform motivationParent;
	public Transform scoreParent;
	public Transform moneyParent;
	public TextMeshProUGUI motivationPrefab;
	public TextMeshProUGUI scorePrefab;
	public TextMeshProUGUI moneyPrefab;
	
	public TextMeshProUGUI score;
	public TextMeshProUGUI best;
	public TextMeshProUGUI money;
	
	public TextMeshProUGUI currentLevel;
	public TextMeshProUGUI nextLevel;
	public Image bar;

	public GameObject[] splashEffectPrefabs;
	public Transform splashEffectParent;
	
	public Transform splashLocationsParent;
	private Transform[] splashLocations;
	
	public string firstMotivationText = "Nice!";
	public string secondMotivationText = "Great!";
	public string[] motivationTexts;
	
	private GameManager gameManager;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		if (Instance != this)
		{
			Destroy(this);
		}
	}

	void Start()
	{
		splashLocations = new Transform[splashLocationsParent.childCount];
		
		for (int i = 0; i < splashLocationsParent.childCount; i++)
		{
			splashLocations[i] = splashLocationsParent.GetChild(i);
		}
		
		gameManager = GameManager.Instance;
	}

	
	
	void Update()
	{
		score.text = PlayerStats.Score + "";
		best.text = "best: " + PlayerStats.Best + "";
		money.text = intToSimple(PlayerStats.Money);
		
		currentLevel.text = PlayerStats.Level + "";
		nextLevel.text = PlayerStats.Level + 1 + "";

		bar.DOFillAmount(
			(float) PlayerStats.SodaCanCount / (gameManager.levelMilestone + gameManager.levelHardnessMultipler),
			0.25f);
		
	}

	public void CreateScorePopUp(int _score)
	{
		TextMeshProUGUI t = Instantiate(scorePrefab, scoreParent);
		t.text = "+" + _score;
		Destroy(t.gameObject, 3f);
	}
	
	public void CreateMoneyPopUp()
	{
		TextMeshProUGUI t = Instantiate(moneyPrefab, moneyParent);
		t.text = "+" + PlayerStats.Multiplier;
		Destroy(t.gameObject, 3f);
	}
	
	public void CreateMotivationPopUp(int multiplier)
	{
		TextMeshProUGUI t = Instantiate(motivationPrefab, motivationParent);

		if (multiplier == 2)
		{
			t.text = firstMotivationText;
		}
		else if (multiplier == 3)
		{
			t.text = secondMotivationText;
		}
		else if (multiplier >= 4)
		{
			int randIndex = Random.Range(0, motivationTexts.Length);
			t.text = motivationTexts[randIndex];
		}

		float randRotation = Random.Range(-15f, 15f);
		t.transform.rotation = Quaternion.Euler(0,0,randRotation);

		Destroy(t.gameObject, 3f);
	}

	public void CreateSplashEffect(Color _color)
	{
		int splashCount = Random.Range(1,3);
		
		for (int i = 0; i < splashCount; i++)
		{
			int randPrefabIndex = Random.Range(0,splashEffectPrefabs.Length);
		
			GameObject splash = Instantiate(splashEffectPrefabs[randPrefabIndex], splashEffectParent);
			Transform splashGFX = splash.transform.GetChild(0);
		
			float randRotation = Random.Range(-180f, 180f);
			splashGFX.transform.rotation = Quaternion.Euler(0,0,randRotation);

			float randScale = Random.Range(1.5f,3f);
			splashGFX.transform.localScale = Vector3.one * randScale;

			int randIndex = Random.Range(0, splashLocations.Length);
			splashGFX.transform.position = splashLocations[randIndex].position;
		
			splashGFX.GetComponent<Image>().color = _color;
		
			Destroy(splash, 3f);
		}
	}
	
	private string intToSimple(int number)
	{
		if (number >= 100000000)
		{
			return (number / 100000D).ToString("0.#M");
		}
		if (number >= 1000000) {
			return (number / 1000000D).ToString("0.#M");
		}
		if (number >= 100000) {
			return (number / 1000D).ToString("0.#k");
		}
		
		if (number >= 10000) {
			return (number / 1000D).ToString("0.#k");
		}

		if (number >= 1000)
		{
			return (number / 1000D).ToString("0.#k");
		}

		return number.ToString("#,0");
	}
}
