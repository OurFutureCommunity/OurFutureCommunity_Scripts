using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartingSceneManager : MonoBehaviour
{
	[SerializeField] LevelLoader levelLoader;
	public GameObject continueBtns;
	public GameObject startBtn;
	public TextMeshProUGUI startText;
	public GlobalControl globalControl;

    // Start is called before the first frame update
    void Awake()
    {
		startBtn.SetActive(false);
		continueBtns.SetActive(false);
		//Debug.Log(globalControl.inputName);
		if(PlayerPrefs.GetInt("InputName") == 1)//if(globalControl.inputName == 1)
		{
			startText.text = "Welcome back " + PlayerPrefs.GetString("Name") + "!";//globalControl.playerName + "!";
			continueBtns.SetActive(true);			
		}
		else
		{
			startText.text = "Our Future Community";	
			startBtn.SetActive(true);
		}
		
    }
    
    void Update()
    {
        
    }
		
}
