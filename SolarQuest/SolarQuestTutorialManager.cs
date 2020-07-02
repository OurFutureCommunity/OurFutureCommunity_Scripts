using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SolarQuest
{
    public class SolarQuestTutorialManager : MonoBehaviour
    {

        #region Cameras
        [SerializeField] Camera startCam;
        [SerializeField] Camera questCam;
        [SerializeField] Camera sliderTutorialCam;
        #endregion

        public float score;
        [SerializeField] GameObject character;

        // START
        [SerializeField] GameObject startCanvas;

        // INTRODUCTION
        [SerializeField] GameObject introCanvas;
        //public bool introPlayed;

		// SLIDER
		[SerializeField] GameObject sliderTutorialCanvas;
        [SerializeField] GameObject introPopup;
        [SerializeField] GameObject sliderUI;
        [SerializeField] GameObject sliderPopup;
        [SerializeField] GameObject compassPopup;
        [SerializeField] GameObject sliderDoneButton;

		// QUIZ
		[SerializeField] GameObject quizCanvas;
        [SerializeField] GameObject quizInfo;
        [SerializeField] GameObject quizPanel;
        [SerializeField] GameObject backButton;

		// SOLAR GAME
		[SerializeField] GameObject solarGameCanvas;
        [SerializeField] GameObject solarGame;
        [SerializeField] GameObject solarGameDoneButton;
        [SerializeField] GameObject budget;
        [SerializeField] GameObject energyBar;
        [SerializeField] GameObject introBox;
        [SerializeField] GameObject solarPanelsPopup;
        [SerializeField] GameObject budgetPopup;
        [SerializeField] GameObject energyPopup;

		// END
		[SerializeField] GameObject endCanvas;
        [SerializeField] GameObject endTextBox;
        [SerializeField] TextMeshProUGUI endText;
        [SerializeField] GameObject choiceButtons;

        // Lights
        [SerializeField] Light mainLight;
        [SerializeField] Light sliderLight;





        public enum GameState
        {
            Start,
            Introduction,
            SliderTutorial,
            Quiz,
            SolarGame,
            End
        }

        private GameState currentState;

        private void Start()
        {
            character.SetActive(false);

            // Start out using tutorial camera
            startCam.enabled = true;
            questCam.enabled = false;
            sliderTutorialCam.enabled = false;

            // Light set up
            mainLight.enabled = true;
            sliderLight.enabled = false;

            // INTRODUCTION
            introCanvas.SetActive(false);

            // SLIDER TUTORIAL
            introPopup.SetActive(false);
            sliderUI.SetActive(false);
            sliderPopup.SetActive(false);
            compassPopup.SetActive(false);
            sliderDoneButton.SetActive(false);

			// QUIZ
			quizCanvas.SetActive(false);
            quizInfo.SetActive(false);
            quizPanel.SetActive(false);
            backButton.SetActive(false);

			// SOLAR GAME
			solarGameCanvas.SetActive(false);
            solarGame.SetActive(false);
            budget.SetActive(false);
            energyBar.SetActive(false);
            introBox.SetActive(false);
            solarPanelsPopup.SetActive(false);
            budgetPopup.SetActive(false);
            energyPopup.SetActive(false);
            solarGameDoneButton.SetActive(false);

			// END
			endCanvas.SetActive(false);
            endTextBox.SetActive(false);
            choiceButtons.SetActive(false);

            // Set start state
			//SetState(GameState.Introduction);
            SetGameState();
        }

        void SetGameState()
        {
            if(GlobalControl.Instance.solarTutorialIntroPlayed)
            {
                SetState(GameState.SliderTutorial);
            }
            else{
                SetState(GameState.Introduction);
            }
        }

        public void ChangeStateToIntroduction()
        {
            SetState(GameState.Introduction);
        }

        public void ChangeStateToSliderTutorial()
        {
            SetState(GameState.SliderTutorial);
        }

        public void ChangeStateToQuiz()
        {
            SetState(GameState.Quiz);
        }

        public void ChangeStateToSolarGame()
        {
            SetState(GameState.SolarGame);
        }

        public void ChangeStateToEnd()
        {
            SetState(GameState.End);
        }



        private void SetState(GameState newState)
        {
            ExitCurrentState();
            currentState = newState;
            switch (currentState)
            {
                case GameState.Start:
                    StartState();
                    break;
                case GameState.Introduction:
                    IntroductionState();
                    break;
                case GameState.SliderTutorial:
                    SliderTutorial();
                    break;
                case GameState.Quiz:
                    QuizState();
                    break;
                case GameState.SolarGame:
                    SolarGameState();
                    break;
                case GameState.End:
                    EndState();
                    break;
                default:
                    break;
            }
        }

        private void ExitCurrentState()
        {
            switch (currentState)
            {
                case GameState.Start:
                    ExitStartState();
                    break;
                case GameState.Introduction:
                    ExitIntroductionState();
                    break;
                case GameState.SliderTutorial:
                    ExitSliderTutorialState();
                    break;
                case GameState.Quiz:
                    ExitQuizState();
                    break;
                case GameState.SolarGame:
                    ExitSolarGameState();
                    break;
                case GameState.End:
                    ExitEndState();
                    break;
                default:
                    break;
            }
        }

		#region StartStae
		// State at the start with only start button
		private void StartState()
        {
            // Set up cameras
            startCam.enabled = true;
            questCam.enabled = false;
            sliderTutorialCam.enabled = false;

            startCanvas.SetActive(true);
        }

        private void ExitStartState()
        {
            startCanvas.SetActive(false);
        }
		#endregion StartState

		#region IntroductionState
		private void IntroductionState()
        {
            // Set up cameras
            startCam.enabled = true;
            questCam.enabled = false;
            sliderTutorialCam.enabled = false;

            character.SetActive(true);

            introCanvas.SetActive(true);
			introCanvas.GetComponentInChildren<SolarQuestIntroductionBox>().LoadText();
            GlobalControl.Instance.solarTutorialIntroPlayed = true;
        }

        private void ExitIntroductionState()
        {
            character.SetActive(false);
            introCanvas.SetActive(false);
        }
		#endregion IntroductionState

		#region SliderTutorialState
		private void SliderTutorial()
        {
            startCam.enabled = false;
            questCam.enabled = false;
            sliderTutorialCam.enabled = true;

			sliderTutorialCanvas.SetActive(true);
            sliderUI.SetActive(true);

            mainLight.enabled = false;
            sliderLight.enabled = true;

            sliderPopup.SetActive(true);
            //StartCoroutine("SliderPopup");
        }

        IEnumerator SliderPopup()
        {
            introPopup.SetActive(true);
            yield return new WaitForSeconds(3f);
            introPopup.SetActive(false);
            //sliderPopup.SetActive(true);

        }

        public void CloseSliderPopUp()
        {
            sliderPopup.SetActive(false);
            compassPopup.SetActive(true);
        }

        public void CloseCompassPopUp()
        {
            compassPopup.SetActive(false);
            sliderDoneButton.SetActive(true);

            StartCoroutine("SliderPopup");
        }

        private void ExitSliderTutorialState()
        {
            sliderUI.SetActive(false);
            sliderDoneButton.SetActive(false);
			sliderTutorialCanvas.SetActive(false);

			mainLight.enabled = true;
            sliderLight.enabled = false;
        }
		#endregion SliderTutorialState

		#region QuizState
		private void QuizState()
        {
            startCam.enabled = false;
            questCam.enabled = false;
            sliderTutorialCam.enabled = true;

			quizCanvas.SetActive(true);
            quizInfo.SetActive(true);
            backButton.SetActive(true);

            character.SetActive(true);
        }

        public void LoadQuizChoices()
        {
            quizInfo.SetActive(false);
            quizPanel.SetActive(true);
        }

        private void ExitQuizState()
        {
            quizInfo.SetActive(false);
            quizPanel.SetActive(false);
            backButton.SetActive(false);
			quizCanvas.SetActive(false);
            character.SetActive(false);
        }
		#endregion QuizState

		#region SolarGameState
		private void SolarGameState()
        {
            startCam.enabled = false;
            questCam.enabled = true;
            sliderTutorialCam.enabled = false;

			solarGameCanvas.SetActive(true);
            energyBar.SetActive(true);
            budget.SetActive(true);
            introBox.SetActive(true);
            solarGame.SetActive(true);
            //solarGameDoneButton.SetActive(true);
        }

        public void CloseIntroBox()
        {
            introBox.SetActive(false);
            solarPanelsPopup.SetActive(true);            
            
        }

        public void CloseSolarPanelsPopup()
        {
            solarPanelsPopup.SetActive(false);
            budgetPopup.SetActive(true);
        }

        public void CloseBudgetPopup()
        {
            budgetPopup.SetActive(false);
            energyPopup.SetActive(true);
        }

        public void CloseEnergyPopup()
        {
            energyPopup.SetActive(false);
            solarGameDoneButton.SetActive(true);
        }

        private void ExitSolarGameState()
        {
            if (SolarScoring.Instance)
            {
                score = SolarScoring.Instance.energyScore;
            }
            introBox.SetActive(false);
            energyBar.SetActive(false);
            budget.SetActive(false);
            solarGame.SetActive(false);
            solarGameDoneButton.SetActive(false);
            solarPanelsPopup.SetActive(false);
            budgetPopup.SetActive(false);
            energyPopup.SetActive(false);
			
            if (SolarGamePopupManager.Instance)
            {
                SolarGamePopupManager.Instance.CloseAllPopups();
            }

			solarGameCanvas.SetActive(false);
		}
		#endregion SolarGameState

		#region EndState
		private void EndState()
        {
            startCam.enabled = false;
            questCam.enabled = true;
            sliderTutorialCam.enabled = false;

			endCanvas.SetActive(true);
            character.SetActive(true);
            endTextBox.SetActive(true);
            

            choiceButtons.SetActive(true);
            endText.text = "Congratulations! You managed to achieve " + Mathf.RoundToInt(score* 100) + "% of the total energy potential. Would you like to try again or continue?";
        }

        private void ExitEndState()
        {
            choiceButtons.SetActive(false);
            endTextBox.SetActive(false);
            character.SetActive(false);
			endCanvas.SetActive(false);
        }

        public void LoadEndText()
        {
            choiceButtons.SetActive(false);
			EndTextBox endTextBoxScript = endTextBox.GetComponent<EndTextBox>();
			endTextBoxScript.LoadText();
			endTextBoxScript.tapToContinueText.SetActive(true);
			endTextBoxScript.imageHolder.SetActive(true);
        }
		#endregion EndState

	}

}

