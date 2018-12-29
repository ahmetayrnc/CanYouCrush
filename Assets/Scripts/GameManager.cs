using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SodaCanMovement sodaCanMovement;
    public LevelColorController levelColorController;
    
    public ScoreUI scoreUI;
    public GameOverUI gameOverUI;
    public LevelTransitionUI levelTransitionUI;
    public TapToStartUI tapToStartUI;
    
    public int levelMilestone = 15;
    
    public bool gameIsOver;
    public bool gamePaused;

    public int levelHardnessMultipler;

    public bool mainMenu;
    
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
        Application.targetFrameRate = 60;
    }
    
    void Start()
    {
        mainMenu = MainMenuTracker.Instance.mainMenu;
        
        if (mainMenu)
        {
            tapToStartUI.gameObject.SetActive(true);
            scoreUI.gameObject.SetActive(false);
            gamePaused = true;
        }
        else
        {
            tapToStartUI.gameObject.SetActive(false);
            scoreUI.gameObject.SetActive(true);
            gamePaused = false;
            
            //Voodoo
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        }
        gameOverUI.gameObject.SetActive(false);
        
    }
	
    void Update ()
    {
        levelHardnessMultipler = (int)Mathf.Pow(Mathf.Log(PlayerStats.Level),2);
        
        if (gameIsOver)
        {
            return;
        }

        if (gamePaused)
        {
            return;
        }

        if (PlayerStats.SodaCanCount >= levelMilestone + levelHardnessMultipler)
        {
            StartCoroutine(ChangeLevel());
        }
    }
    
    public void EndGame()
    {
        gameIsOver = true;
        
        gameOverUI.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(false);
        
        if (PlayerStats.Score > PlayerPrefs.GetInt("best"))
        {
            PlayerPrefs.SetInt("best", PlayerStats.Score);
            PlayerPrefs.Save();
            
            gameOverUI.ShowBest();
        }
        
        PlayerPrefs.SetInt("Money", PlayerStats.Money);
        PlayerPrefs.Save();
        
        PlayerStats.ResetMultiplier();
        
        //voodoo
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", PlayerStats.Score);
        
        //me
        GameAnalytics.NewDesignEvent("factory", PlayerStats.BackgroundLevel);
        GameAnalytics.NewDesignEvent("income", PlayerStats.IncomeLevel);
        GameAnalytics.NewDesignEvent("level", PlayerStats.Level);
        GameAnalytics.NewDesignEvent("money", PlayerStats.Money);
        GameAnalytics.NewDesignEvent("best", PlayerStats.Best);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator ChangeLevel()
    {
        gamePaused = true;
        
        PlayerStats.ResetSodaCanCount();
        PlayerStats.IncrementLevel();
        
        PlayerPrefs.SetInt("Level", PlayerStats.Level);
        PlayerPrefs.Save();
        
        CrusherUnlock.Instance.CheckUnlock();
        ObjectPooler.Instance.UnlockNextObject();
        
        if (PlayerStats.Level % ObjectPooler.Instance.unlockRate == 0)
        {
            levelTransitionUI.ShowUnlockObject();
        }
        
        levelTransitionUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        levelTransitionUI.gameObject.SetActive(false);
        
        
        levelTransitionUI.HideUnlockObject();
        
        sodaCanMovement.NewLevel();

        foreach (Transform t in sodaCanMovement.transform)
        {
            if (t.CompareTag("Object"))
            {
                ObjectPooler.Instance.DestroyObject(t.gameObject);
            }
        }
        
        levelColorController.ChangeColor();
        
        BackgroundManager.Instance.SetupBackground();
        
        gamePaused = false;
    }
}
