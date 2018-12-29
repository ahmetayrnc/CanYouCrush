using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Score;
    public static int Best;
    public static int Multiplier;
    public static int SodaCanCount;
    public static int Level;
    public static int BackgroundLevel;
    public static int IncomeLevel;
    public static int Money;
    public static int Price;
    public static int PricePassive;
    
    void Start()
    {
        Score = 0;
        Multiplier = 1;
        SodaCanCount = 0;

        if (!PlayerPrefs.HasKey("best"))
        {
            PlayerPrefs.SetInt("best",0);
            PlayerPrefs.Save();
        }
        
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level",1);
            PlayerPrefs.Save();
        }
        
        if (!PlayerPrefs.HasKey("BackgroundLevel"))
        {
            PlayerPrefs.SetInt("BackgroundLevel",0);
            PlayerPrefs.Save();
        }
        
        if (!PlayerPrefs.HasKey("IncomeLevel"))
        {
            PlayerPrefs.SetInt("IncomeLevel",0);
            PlayerPrefs.Save();
        }
        
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money",0);
            PlayerPrefs.Save();
        }
        
        Best = PlayerPrefs.GetInt("best");
        Level = PlayerPrefs.GetInt("Level");
        
        BackgroundLevel = PlayerPrefs.GetInt("BackgroundLevel");
        IncomeLevel = PlayerPrefs.GetInt("IncomeLevel");
        
        Money = PlayerPrefs.GetInt("Money");

        UpdatePrices();
    }

    public static void IncrementSodaCanCount()
    {
        SodaCanCount++;
    }
    
    public static void ResetSodaCanCount()
    {
        SodaCanCount = 0;
    }
    
    public static void IncrementScore()
    {
        Score += Multiplier * Level;
        Money += Multiplier;
    }

    public static void IncrementMultiplier()
    {
        Multiplier += 1;
    }

    public static void ResetMultiplier()
    {
        Multiplier = 1;
    }

    public static void IncrementLevel()
    {
        Level++;
    }

    public static void IncrementBackgroundLevel()
    {
        BackgroundLevel++;
        PlayerPrefs.SetInt("BackgroundLevel", BackgroundLevel);
        PlayerPrefs.Save();
        
        BackgroundManager.Instance.SetupBackground();
    }

    public static void IncrementPassiveIncome()
    {
        IncomeLevel++;
        PlayerPrefs.SetInt("IncomeLevel", IncomeLevel);
        PlayerPrefs.Save();
    }

    public static void UpdatePrices()
    {
        Price = 100 * (int)Mathf.Pow(1.9f, BackgroundLevel + 1);
        PricePassive = 100 * (int) Mathf.Pow(2.1f, IncomeLevel + 1);
    }
}
