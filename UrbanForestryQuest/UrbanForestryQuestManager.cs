using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UrbanForestryQuest
{
    public class UrbanForestryQuestManager : MonoBehaviour
    {
		[SerializeField] GameObject topDownCam; 
		[SerializeField] GameObject frontCam;

        [SerializeField] GameObject introCanvas;
        [SerializeField] GameObject quizCanvas;
        [SerializeField] GameObject uiCanvas;
        [SerializeField] GameObject tutorialCanvas;
        [SerializeField] GameObject endCanvas;
        //[SerializeField] GameObject cameraCanvas;

        [SerializeField] GameObject oopsTextbox;
        [SerializeField] GameObject endTextbox;
        [SerializeField] TextMeshProUGUI endText;
        
        //[SerializeField] GameObject switchCameraButtons;
        
        [SerializeField] GameObject treePlantingScript;

		public enum GameState
		{
			Introduction,
            Quiz,
			Tutorial,
			PlantTrees,
			End
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

        #region Singleton
        private static UrbanForestryQuestManager instance = null;
        public static UrbanForestryQuestManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }
        #endregion

        // CHANGE CURRENT STATE HERE FOR TESTING PURPOSES
        void Start()
        {
            QuestInitialSetUp();

			CurrentState = GameState.Introduction;
        }

        private void QuestInitialSetUp()
        {
            oopsTextbox.SetActive(false);
            endTextbox.SetActive(false);

            introCanvas.SetActive(false);
            quizCanvas.SetActive(false);
            uiCanvas.SetActive(false);
            tutorialCanvas.SetActive(false);
            endCanvas.SetActive(false);
            //cameraCanvas.SetActive(false);

			// cams
			topDownCam.SetActive(false);
			frontCam.SetActive(false);
            //switchCameraButtons.SetActive(false);

            noPlantingTreesZone.SetActive(true);
        }



        private void SetState(GameState newState)
        {
            
            switch (newState)
            {
                case GameState.Introduction:
                    HandleIntroductionState_On();
                    break;
                case GameState.Quiz:
                    HandleQuizState_On();
                    break;
                case GameState.Tutorial:
                    HandleTutorialState_On();
                    break;
                case GameState.PlantTrees:
                    HandlePlantTreesState_On();
                    break;
                case GameState.End:
                    HandleEndState_On();
                    break;
                default:
                    break;
            }
        }

        private void ClearOldState(GameState oldState)
        {
            switch (oldState)
            {
                case GameState.Introduction:
                    HandleIntroductionState_Off();
                    break;
                case GameState.Quiz:
                    HandleQuizState_Off();
                    break;
                case GameState.Tutorial:
                    HandleTutorialState_Off();
                    break;
                case GameState.PlantTrees:
                    HandlePlantTreesState_Off();
                    break;
                case GameState.End:
                    HandleEndState_Off();
                    break;
                default:
                    break;
            }
        }

        #region IntroductionState
        private void HandleIntroductionState_On()
        {
            introCanvas.SetActive(true);
            topDownCam.SetActive(false);
            frontCam.SetActive(true);
        }


        private void HandleIntroductionState_Off()
        {
			introCanvas.SetActive(false);	

        }
        #endregion

        #region QuizState

        private void HandleQuizState_On()
        {
            quizCanvas.SetActive(true);
        }


        private void HandleQuizState_Off()
        {
            quizCanvas.SetActive(false);
        }

        #endregion

        #region TutorialState
        private void HandleTutorialState_On()
        {
			StartCoroutine(LookAtCanopy());            
        }

		private IEnumerator LookAtCanopy()
		{
			yield return new WaitForSeconds(.1f);
			uiCanvas.SetActive(true);
			tutorialCanvas.SetActive(true);
			topDownCam.SetActive(true);
			frontCam.SetActive(false);
			//cameraCanvas.SetActive(true);

			tutorialCanvas.GetComponentInChildren<TutorialPopup>().InitializeTutorial();
		}

        private void HandleTutorialState_Off()
        {
            uiCanvas.SetActive(false);
            tutorialCanvas.SetActive(false);
        }
        #endregion

        #region PlantTrees
        private void HandlePlantTreesState_On()
        {
            uiCanvas.SetActive(true);
            topDownCam.SetActive(true);
            frontCam.SetActive(false);
        }

        private void HandlePlantTreesState_Off()
        {
            uiCanvas.SetActive(false);
        }

        public void PlantTrees()
        {
            StopAllCoroutines();
            CurrentState = GameState.PlantTrees;
            LevelManager.GetInstance().ResetLevelObjects();
            endCanvas.SetActive(false);
            endTextbox.SetActive(false);
        }
        #endregion

        #region EndState

        [SerializeField] GameObject proceedButton;
        [SerializeField] GameObject scoreBox;
        TextMeshProUGUI scoreBoxText;
        [SerializeField] TextMeshProUGUI oopsText;
        [SerializeField] GameObject fadeAwayBackground;
        [SerializeField] float fadeOutTime;
        [SerializeField] GameObject noPlantingTreesZone;

        public void SwitchToEndState()
        {
            treePlantingScript.GetComponent<PlantTrees>().CloseAllModes();
            //Debug.Log("End state");
			CurrentState = GameState.End;


			/*
            int score = Mathf.RoundToInt(LevelManager.GetInstance().CanopyScore);
            
            if (score <= 15)
            {
                oopsText.text = "<b>Oops! You must play again.</b> You only achieved " + score + "% canopy cover for your block, which is only " + (score - 9) + "% more than what you started.";
                oopsTextbox.SetActive(true);
            }
            else
            {
                treePlantingScript.GetComponent<PlantTrees>().CloseAllModes();
                CurrentState = GameState.End;
            }*/
        }

        public void CloseOops()
        {
            oopsTextbox.SetActive(false);
        }

        private void HandleEndState_On()
        {
            StopAllCoroutines();
            endCanvas.SetActive(true);
            

            //cameraCanvas.SetActive(true);
            //switchCameraButtons.SetActive(true);
            /*
            scoreBox.SetActive(true);
            proceedButton.SetActive(true);
            

            scoreBoxText = scoreBox.GetComponentInChildren<TextMeshProUGUI>();
            int canopyScore = Mathf.RoundToInt(LevelManager.GetInstance().CanopyScore);
            scoreBoxText.text = GetEndText(canopyScore);
			*/
            StartCoroutine(FadeAway(fadeAwayBackground));
			LevelManager.GetInstance().KillNonperableGrowLiveTrees();
			LevelManager.GetInstance().VisualizeFuture();
			StartCoroutine(LookAtFuture());
        }

        private void HandleEndState_Off()
        {

        }

        private string GetEndText(int score)
        {
           
            string endText = "";
            if (score > 15 && score < 22)
            {
                endText = "<b>You're almost there!</b> You achieved " + score + "% canopy cover for this house. You missed the City's target by " + (22 - score) + "%";
            }
            if (score >= 22 && score < 40)
            {
                endText = "<b>You did great!</b> You achieved " + score + "% canopy cover for this house. You surpassed the City's target by " + (score - 22) + "%";
            }
            else if (score >= 40)
            {
                endText = "<b>Wow, " + score + "%! You’re a Champion!</b> This is now the Greenest Block in Vancouver!";
            }
            else
            {
                endText =  "<b>Hmm, you're not quite there yet.</b> You achieved " + score + "% Let's try again!";
            }
            
            return endText;
        }

        public void CloseScoreBox()
        {
            scoreBox.SetActive(false);
        }

        public void ProceedToEndText()
        {
            //switchCameraButtons.SetActive(false);
            //endText.text ="The canopy cover is "+  LevelManager.GetInstance().canopyCoverPercentage.ToString() + "%. Would you like to try again?";
            endText.text = GetEndText(LevelManager.GetInstance().canopyCoverPercentage);
            proceedButton.SetActive(false);
            scoreBox.SetActive(false);
            endTextbox.SetActive(true);
        }

        private IEnumerator FadeAway(GameObject objectToFade)
        {
            //Debug.Log("fade");
            CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();

            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Min(1, t / fadeOutTime));
                yield return null;
            }
            canvasGroup.alpha = 0;
            objectToFade.SetActive(false);

        }

		private IEnumerator LookAtFuture()
		{			
			//yield return new WaitForSeconds(.2f);
			//noPlantingTreesZone.SetActive(true);
			//Debug.Log("See Future");
			yield return new WaitForSeconds(3f);
			//topDownCam.SetActive(false);
			//frontCam.SetActive(true);
			ProceedToEndText();

		}
        #endregion
    }
}


