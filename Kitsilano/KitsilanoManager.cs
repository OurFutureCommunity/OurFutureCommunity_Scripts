using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

namespace KitsilanoScene
{
    public class KitsilanoManager : MonoBehaviour
    {
		public GameObject topDownCamera;
		public GameObject cineCamera;
		public GameObject startSequenceCanvas;
		public TextMeshProUGUI nameTextField;
		public TextMeshProUGUI startSequenceText;
        public GameObject logoCanvas;
        public GameObject buttonCanvas;
        public GameObject infoCanvas;
        public GameObject welcomeMessage;
		public GameObject streetSigns;

		public GameObject introDialog; 

		public GameObject introductionSequence;

		public PlayableDirector cutscene;
		public PlayableDirector logoTransition;

		public GameObject creditSequence;

		public enum GameState
		{
			
			StartGame,
			StartSequence,
            IntroCutscene,
			MainMenu,
			EndCutscene,
			Credits,
		}

		private GameState currentState;

        public GameState CurrentState
        {
            get { return currentState; }
            set
            {
                ClearOldState(currentState);
                currentState = value;
                SetState(currentState);
            }
        }

		
        private void SetState(GameState newState)
        {
            
            switch (newState)
            {
				case GameState.StartGame:
				HandleStartGame_On();
					break;
                case GameState.StartSequence:
					HandleStartSequenceState_On();
                    break;
                case GameState.IntroCutscene:
					HandleIntroCutsceneState_On();
                    break;
                case GameState.MainMenu:
					HandleMainMenuState_On();
                    break;
				case GameState.EndCutscene:
					HandleEndCutsceneState_On();
					break;
				case GameState.Credits:
					HandleCreditsState_On();
					break;	
				default:
				break;
            }
        }

        private void ClearOldState(GameState oldState)
        {
            switch (oldState)
            {
				case GameState.StartGame:
				HandleStartGame_Off();
					break;
                case GameState.StartSequence:
					HandleStartSequenceState_Off();
                    break;
                case GameState.IntroCutscene:
					HandleIntroCutsceneState_Off();
                    break;
                case GameState.MainMenu:
					HandleMainMenuState_Off();
                    break;
				case GameState.EndCutscene:
					HandleEndCutsceneState_Off();
					break;
				case GameState.Credits:
					HandleCreditsState_Off();
					break;

                default:
                    break;
            }
        }


        #region Singleton
        private static KitsilanoManager instance = null;
        public static KitsilanoManager GetInstance()
        {
            return instance;
        }

        private void Start()
        {
            instance = this;
        }
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
				
			
			topDownCamera.SetActive(true);

			cineCamera.SetActive(false);
			logoCanvas.SetActive(false);
			buttonCanvas.SetActive(false);
			infoCanvas.SetActive(false);
			startSequenceCanvas.SetActive(false);
			introductionSequence.SetActive(false);	
			creditSequence.SetActive(false);

			//CurrentState = GameState.EndCutscene;   // CHANGE THISS -------------------------------------------------------------
			OnStartGame();
			//CurrentState = GameState.StartSequence;
        }

		public void OnStartGame()
		{
			// Play end cutscene
			if(GlobalControl.Instance.solarQuestPlayed && GlobalControl.Instance.urbanForestryPlayed && !GlobalControl.Instance.endCutScenePlayed)
			{
				CurrentState = GameState.EndCutscene;
			}
			// Main Menu
			else if (GlobalControl.Instance.introCutscenePlayed || GlobalControl.Instance.inputName == 1)
			{
				CurrentState = GameState.MainMenu;
			}
            else
			{
				CurrentState = GameState.StartSequence;
			}
		}

		void HandleStartGame_On()
		{
			//StartButton.SetActive(true);
			//pushedStartGame = true;
			//streetSigns.SetActive(true);
		}

		void HandleStartGame_Off()
		{
			Debug.Log("start off");
			//streetSigns.SetActive(false);
			//StartButton.SetActive(false);	
		}



        public void CloseWelcomeMessage()
        {
            welcomeMessage.SetActive(false);
            buttonCanvas.SetActive(true);
            infoCanvas.SetActive(true);
        }

		private void OnEnable()
		{
			cutscene.stopped += OnCutsceneEnd;
			//logoTransition.stopped += OnLogoTransitionEnd;
		}

		void OnCutsceneEnd(PlayableDirector aDirector)
		{
			if (cutscene == aDirector ) //&& !GlobalControl.Instance.introCutscenePlayed
			{
				Debug.Log("Cut Scene End, go to logo");
                StartCoroutine(StartLogoTransition());
			}/*
			else
			{
				Debug.Log("Cut Scene End, go to main menu");
				CurrentState = GameState.MainMenu;
			}*/
		}


		void OnDisable()
		{
			cutscene.stopped -= OnCutsceneEnd;
			//logoTransition.stopped += OnLogoTransitionEnd;
		}

        private IEnumerator StartLogoTransition()
		{
			introductionSequence.SetActive(false);
			Debug.Log("Logo Transition");
            yield return new WaitForSeconds(2f);
			//yield return null;
			logoCanvas.SetActive(true);
			logoTransition.Play();
		}

        public void SkipIntroduction()
		{
			GlobalControl.Instance.introCutscenePlayed = true;
			introductionSequence.SetActive(false);
			//StartLogoTransition();
			StartCoroutine(StartLogoTransition());
		}

		void HandleStartSequenceState_On()
		{
			topDownCamera.SetActive(true);
			cineCamera.SetActive(false);
			startSequenceCanvas.SetActive(true);
			streetSigns.SetActive(true);
		}
		void HandleStartSequenceState_Off()
		{
			startSequenceCanvas.SetActive(false);
			streetSigns.SetActive(false);
		}

		void HandleIntroCutsceneState_On()
		{
			
			introductionSequence.SetActive(true);
			Debug.Log("Playing Introduction");
			introductionSequence.GetComponent<IntroductionSequence>().StartIntroductionSequence();
			topDownCamera.SetActive(false);
			cineCamera.SetActive(true);
		}
		void HandleIntroCutsceneState_Off()
		{
			topDownCamera.SetActive(true);
			cineCamera.SetActive(false);
			introductionSequence.SetActive(false);
		}
		void HandleMainMenuState_On()
		{			
			topDownCamera.SetActive(true);
			streetSigns.SetActive(true);
			cineCamera.SetActive(false);

			logoCanvas.SetActive(true);
			buttonCanvas.SetActive(true);
			infoCanvas.SetActive(true);
		}
		void HandleMainMenuState_Off()
		{
			logoCanvas.SetActive(false);
			streetSigns.SetActive(false);
		}

		void HandleEndCutsceneState_On()
		{
			Debug.Log("End cutscene");
			GlobalControl.Instance.endCutScenePlayed = true;
			introductionSequence.SetActive(true);
			introductionSequence.GetComponent<IntroductionSequence>().StartEndSequence();

			//topDownCamera.SetActive(false);
		}

		void HandleEndCutsceneState_Off()
		{
			introductionSequence.SetActive(false);
			//topDownCamera.SetActive(true);
		}
		
		void HandleCreditsState_On()
		{
			Debug.Log("CREDITS Active");
            if (!GlobalControl.Instance.endCutScenePlayed) { currentState = GameState.MainMenu; }
            Debug.Log("CREDITS START");
            creditSequence.SetActive(true);
			creditSequence.GetComponent<Credits>().RollCredits();
		}

		void HandleCreditsState_Off()
		{
			Debug.Log("CREDITS END");
			creditSequence.SetActive(false);
		}
		public void EnterPlayerName()
		{
			if(nameTextField.text.Length > 1) //nameTextField.text != "" || nameTextField.text != " "
			{
				Debug.Log("start intro");
				GlobalControl.Instance.playerName = nameTextField.text;
				PlayerPrefs.SetString("Name" , nameTextField.text);
				PlayerPrefs.SetInt("InputName",1);
				Debug.Log(GlobalControl.Instance.playerName);
				CurrentState = GameState.IntroCutscene;

			}
			else
			{

				Debug.Log("no name");
				startSequenceText.text = "Please type in your name below!";
			}
		}
	}

}
