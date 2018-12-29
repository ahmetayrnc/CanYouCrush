using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartUI : MonoBehaviour
{
	public TextMeshProUGUI money;
	public TextMeshProUGUI factoryPrice;
	public TextMeshProUGUI passicePrice;

	public TextMeshProUGUI factoryLevelText;
	public TextMeshProUGUI incomeLevelText;

	public TextMeshProUGUI passiveIncomeText;
	public Transform passiveIncomePanel;
	
	public Transform title;

	public Transform GFXParent;

	public Image factoryButton;
	public Image incomeButton;
	
	public Color activeColor;
	public Color inactiveColor;
	
	private int currentCrusher;

	private int activeCrusher;
	
	void Start()
	{
		if (!PlayerPrefs.HasKey("Crusher"))
		{
			PlayerPrefs.SetInt("Crusher",0);
			PlayerPrefs.Save();
		}

		currentCrusher = PlayerPrefs.GetInt("Crusher");

		for (int i = 0; i < GFXParent.childCount; i++)
		{
			GFXParent.GetChild(i).gameObject.SetActive(false);
			if (currentCrusher == i)
			{
				GFXParent.GetChild(i).gameObject.SetActive(true);
			}
		}
	}
	
	public void Update()
	{
	    money.text = intToSimple(PlayerStats.Money);

		if (PlayerStats.Money >= PlayerStats.Price)
		{
			factoryButton.color = activeColor;
		}
		else
		{
			factoryButton.color = inactiveColor;
		}
		
		if (PlayerStats.Money >= PlayerStats.PricePassive)
		{
			incomeButton.color = activeColor;
		}
		else
		{
			incomeButton.color = inactiveColor;
		}
		
		if (PlayerStats.BackgroundLevel >= 11)
		{
			factoryLevelText.text = "Factory" + "\n" + "Level " + "MAX";
			factoryPrice.text = "MAX";
		}
		else
		{
			factoryLevelText.text = "Factory" + "\n" + "Level " + PlayerStats.BackgroundLevel;
			factoryPrice.text = "$" + intToSimple(PlayerStats.Price);
		}

		if (PlayerStats.IncomeLevel >= 11)
		{
			incomeLevelText.text = "Offline Earnings" + "\n" + "Level " + "MAX";
			passicePrice.text = "MAX";
		}
		else
		{
			incomeLevelText.text = "Offline Earnings" + "\n" + "Level " + PlayerStats.IncomeLevel;
			passicePrice.text = "$" + intToSimple(PlayerStats.PricePassive);
		}
	}
	
	public void StartGame()
	{
		MainMenuTracker.Instance.mainMenu = false;

		if (GFXParent.GetChild(currentCrusher).GetComponent<Crusher>().unlocked)
		{
			PlayerPrefs.SetInt("Crusher",currentCrusher);
			PlayerPrefs.Save();
		}
		
		GameManager.Instance.RestartGame();
	}

	public void IncreaseFactoryLevel()
	{
		if (PlayerStats.BackgroundLevel >= 11)
		{
			return;
		}
		
		if (PlayerStats.Money >= PlayerStats.Price)
		{
			PlayerStats.Money -= PlayerStats.Price;
			PlayerStats.IncrementBackgroundLevel();
			PlayerStats.UpdatePrices();
			
			PlayerPrefs.SetInt("Money",PlayerStats.Money);
			PlayerPrefs.Save();
		}
	}

	public void IncreasePassiveIncome()
	{
		if (PlayerStats.IncomeLevel >= 11)
		{
			return;
		}
		
		if (PlayerStats.Money >= PlayerStats.PricePassive)
		{
			PlayerStats.Money -= PlayerStats.PricePassive;
			PlayerStats.IncrementPassiveIncome();
			PlayerStats.UpdatePrices();
			
			PlayerPrefs.SetInt("Money",PlayerStats.Money);
			PlayerPrefs.Save();
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

	public void NextCrusher()
	{
		GFXParent.GetChild(currentCrusher).gameObject.SetActive(false);

		if (currentCrusher + 1 >= GFXParent.childCount)
		{
			currentCrusher = 0;
		}
		else
		{
			currentCrusher = currentCrusher + 1;
		}
		
		GFXParent.GetChild(currentCrusher).gameObject.SetActive(true);
	}

	public void PrevCrusher()
	{
		GFXParent.GetChild(currentCrusher).gameObject.SetActive(false);

		if (currentCrusher - 1 < 0)
		{
			currentCrusher = GFXParent.childCount - 1;
		}
		else
		{
			currentCrusher = currentCrusher - 1;
		}

		GFXParent.GetChild(currentCrusher).gameObject.SetActive(true);
	}

	public void ClosePassiveIncomePanel()
	{
		passiveIncomePanel.gameObject.SetActive(false);
	}

	public void ShowPassiveIncomePanel(int amount)
	{
		passiveIncomePanel.gameObject.SetActive(true);
		passiveIncomeText.text = "$" + intToSimple(amount);
	}
}
