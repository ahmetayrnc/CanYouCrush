using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInit : MonoBehaviour {

	void Awake()
	{
		GameAnalytics.Initialize();
	}
}
