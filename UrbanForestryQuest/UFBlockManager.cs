using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UrbanForestryQuest
{
	public class UFBlockManager : MonoBehaviour
	{
		// ui
		[SerializeField] GameObject budget;
		[SerializeField] GameObject canopyBar;
		[SerializeField] GameObject plantButtons;
		[SerializeField] GameObject mapRightArrow;
		[SerializeField] GameObject mapLeftArrow;
		[SerializeField] GameObject doneButton;
		[SerializeField] GameObject continuePlayingButton;
		[SerializeField] TextMeshProUGUI continueText;		
		[SerializeField] GameObject thisIs2050;
		[SerializeField] GameObject newHighscore;
		[SerializeField] TextMeshProUGUI newHighscoreText;

		// end
		[SerializeField] GameObject endCharacter;
		[SerializeField] GameObject endTextbox;

		[SerializeField] GameObject fadeAwayBackground;
		[SerializeField] float fadeOutTime;

		// Cameras
		[SerializeField] GameObject introCam;
		[SerializeField] GameObject tutorialCam;
		[SerializeField] GameObject camController;

		// Canvas
		[SerializeField] GameObject blockIntroCanvas;
		[SerializeField] GameObject tutorialCanvas;
		[SerializeField] GameObject endCanvas;

		float panSpeed = 0.1f;

		public enum GameState
		{
			Introduction,
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

		private void SetState(GameState newState)
		{

			switch (newState)
			{
			case GameState.Introduction:
				HandleIntroductionState_On();
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

		#region Singleton
		private static UFBlockManager instance = null;
		public static UFBlockManager GetInstance()
		{
			return instance;
		}

		private void Awake()
		{
			instance = this;

			
		}
		#endregion

		
		void Start()
		{
			QuestInitialSetUp();
			if(GlobalControl.Instance.urbanForestryPlayed)
			{
				CurrentState = GameState.PlantTrees;
			}
			else
			{			
				CurrentState = GameState.Introduction;
		    } // CHANGE CURRENT STATE HERE FOR TESTING PURPOSES
			
		}

		private void QuestInitialSetUp()
		{
			// canvas
			blockIntroCanvas.SetActive(false);
			tutorialCanvas.SetActive(false);
			endCanvas.SetActive(false);

			//ui
			budget.SetActive(false);
			canopyBar.SetActive(false);
			plantButtons.SetActive(false);
			mapRightArrow.SetActive(false);
			mapLeftArrow.SetActive(false);
			doneButton.SetActive(false);
			continuePlayingButton.SetActive(false);
			thisIs2050.SetActive(false);
			newHighscore.SetActive(false);
			//cams
			camController.SetActive(false);
			introCam.SetActive(false);
			tutorialCam.SetActive(false);
		}

		#region IntroductionState
		private void HandleIntroductionState_On()
		{
			introCam.SetActive(true);
			blockIntroCanvas.SetActive(true);
			Debug.Log("Intro");
		}

		private void HandleIntroductionState_Off()
		{
			blockIntroCanvas.SetActive(false);
		}
		#endregion

		#region TutorialState
		private void HandleTutorialState_On()
		{
			introCam.SetActive(false);
			Debug.Log("TUTORIAL ON");
			tutorialCam.SetActive(true);
			tutorialCanvas.SetActive(true);
			UFTutorialUIPopUpManager.GetInstance().StartTutorial();

		}

		private void HandleTutorialState_Off()
		{
			Debug.Log("TUTORIAL OFF");
		}
		#endregion

		#region PlantTreesState
		

		void HandlePlantTreesState_On(){
			budget.SetActive(true);
			canopyBar.SetActive(true);
			plantButtons.SetActive(true);
			mapRightArrow.SetActive(true);
			mapLeftArrow.SetActive(true);
			doneButton.SetActive(true);
			camController.SetActive(true);

			introCam.SetActive(false);
			tutorialCam.SetActive(false);
		}
		void HandlePlantTreesState_Off()
		{
			//ui
			budget.SetActive(false);
			canopyBar.SetActive(false);
			plantButtons.SetActive(false);
			mapRightArrow.SetActive(false);
			mapLeftArrow.SetActive(false);
			doneButton.SetActive(false);
		}

		public void ResetPlantTrees()
        {
            StopAllCoroutines();
            LevelManager.GetInstance().ResetLevelObjects();
			thisIs2050.SetActive(false);
			newHighscore.SetActive(false);
            continuePlayingButton.SetActive(false);			
            CurrentState = GameState.PlantTrees;
        }
		#endregion

		#region EndState
		public void SwitchToEndState()
		{
			CurrentState = GameState.End;
		}
		private void HandleEndState_On()
		{
			
			//camController.SetActive(false);
			//tutorialCam.SetActive(true);

			StartCoroutine(FadeAway(fadeAwayBackground));
			LevelManager.GetInstance().KillNonperableGrowLiveTrees();			
			LevelManager.GetInstance().VisualizeFuture();
			thisIs2050.SetActive(true);
			continuePlayingButton.SetActive(true);
			continueText.text = GetEndText(LevelManager.GetInstance().canopyCoverPercentage);
			//"The canopy cover is "+  LevelManager.GetInstance().canopyCoverPercentage.ToString() + "%. Would you like to try again?";
			//StartCoroutine(LookAtFuture());
		}

		public void DisplayNewHighscore(int newHS)
		{
			newHighscore.SetActive(true);
			newHighscoreText.text = newHS.ToString() + "%\n Canopy Cover!";
		}
		private string GetEndText(int score)
        {
           
            string endText = "";
            if (score > 15 && score < 22)
            {
                endText = "<b>You're almost there!</b> You achieved " + score + "% canopy cover for this block. You missed the City's target by " + (22 - score) + "%";
            }
            if (score >= 22 && score < 40)
            {
                endText = "<b>You did great!</b> You achieved " + score + "% canopy cover for this block. You surpassed the City's target by " + (score - 22) + "%";
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

		private IEnumerator LookAtFuture()
		{			
			thisIs2050.SetActive(true);
			yield return new WaitForSeconds(3f);
			continuePlayingButton.SetActive(true);
			/*
			while(tutorialCam.transform.position.x > 20f){
				tutorialCam.transform.position = new Vector3(tutorialCam.transform.position.x - panSpeed, 
					tutorialCam.transform.position.y , tutorialCam.transform.position.z);
				yield return null;
			}
			*/
		}

		public void ProceedToEndText()
		{
			continuePlayingButton.SetActive(false);
			// switch cam from topdown to front
			introCam.SetActive(true);
			tutorialCam.SetActive(false);
			camController.SetActive(false);

			endCanvas.SetActive(true);
			endCharacter.SetActive(true);
			endTextbox.SetActive(true);
		}

		private IEnumerator FadeAway(GameObject objectToFade)
		{
			CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();

			for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
			{
				canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Min(1, t / fadeOutTime));
				yield return null;
			}
			canvasGroup.alpha = 0;
			objectToFade.SetActive(false);
		}

		private void HandleEndState_Off()
		{

		}
		#endregion

		
        public void OnCloseHighScore()
        {
            newHighscore.SetActive(false);
        }
	}
}
