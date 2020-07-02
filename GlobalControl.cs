using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
	public string playerName { get; set;}

    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

		solarQuestHighScore = PlayerPrefs.GetInt("SolarScore", 0);
		ufQuestHighScore = PlayerPrefs.GetInt("UFScore", 0);
		playerName = PlayerPrefs.GetString("Name", "NoName");
		inputName = PlayerPrefs.GetInt("InputName", 0);

        if(solarQuestHighScore > 0)
        {
            solarQuestPlayed = true;
            introCutscenePlayed = true;
        }

        if(ufQuestHighScore > 0)
        {
            urbanForestryPlayed = true;
            introCutscenePlayed = true;
        }

        if(urbanForestryPlayed && solarQuestPlayed)
        {
            endCutScenePlayed = true;
        }
    }
	public bool introCutscenePlayed;

    public bool solarTutorialIntroPlayed;

    public bool solarIntroPlayed;

    public bool solarQuestPlayed;

    public bool urbanForestryPlayed;

    public bool endCutScenePlayed;

	public int inputName;

    // High scores
    public int solarQuestHighScore;
    public int ufQuestHighScore;

    public float typeSpeed;

    public bool debugModeOn;

    public void ResetAllPlayerPrefs()
    {
        //Debug.Log("Hello");
        PlayerPrefs.DeleteAll();
        solarQuestHighScore = PlayerPrefs.GetInt("SolarScore", 0);
		ufQuestHighScore = PlayerPrefs.GetInt("UFScore", 0);
		playerName = PlayerPrefs.GetString("Name", "NoName");
		inputName = PlayerPrefs.GetInt("InputName", 0);

         urbanForestryPlayed = false;
         introCutscenePlayed = false;
         endCutScenePlayed = false;
         solarQuestPlayed = false;

        //Debug.Log("Restart Game");
        //Debug.Log(inputName);
    }


}
