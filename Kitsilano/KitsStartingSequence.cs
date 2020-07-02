using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Android;


namespace KitsilanoScene{
    public class KitsStartingSequence : KitsInfoBox
    {

        public GameObject tapToContinue;
        public GameObject inputName;

        //public GameObject Settings;
        private TouchScreenKeyboard keyboard;

        public TextMeshProUGUI startText;
        // Start is called before the first frame update
        void Start()
        {
            tapToContinue.SetActive(true);            
            inputName.SetActive(false);
            //Settings.SetActive(true);
            
            sentences.Enqueue("Welcome to the Our Future Community (OFC) videogame!");
            sentences.Enqueue("OFC is a place-based educational videogame developed by CALP at UBC to engage its users in climate change education and help them explore LOCAL climate change challenges and solutions.");
            //sentences.Enqueue("This is the Kitsilano neighbourhood in Vancouver. Do you recognize it?");
            //sentences.Enqueue("The game has 2 quests and will take place on 2 different blocks/streets in this neighbourhood.");
            sentences.Enqueue("Let's get started!");
            //sentences.Enqueue("Do you want to explore local climate change challenges and solutions? Type you");
            DisplayNextSentence();
        }
        public override void HandleNoSentencesLeft()
        {
            startText.text = "Type your name below to become part of the game.";
            tapToContinue.SetActive(false);
            inputName.SetActive(true);
            //KitsilanoManager.GetInstance().CurrentState = KitsilanoManager.GameState.IntroCutscene;
        }

        public void OpenKeyboard()
        {
            Debug.Log("Open keyboard");            
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            //keyboard.active = true;
            Debug.Log(keyboard.active);
        }

        public void OnCloseKeyboard()
        {
            
			if(keyboard != null){
				Debug.Log("Close keyboard");
				keyboard.active = false;
                Debug.Log(keyboard.active);
                }
        }
    }
}
