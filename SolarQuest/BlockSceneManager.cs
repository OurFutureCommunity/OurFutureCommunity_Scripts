using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace SolarQuest
{
    public class BlockSceneManager : MonoBehaviour
    {

        #region Singleton
        private static BlockSceneManager instance = null;
        public static BlockSceneManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            GetComponent<HouseSelector>().oHouseSelected += DecrementHousesLeft;
            instance = this;
        }
        #endregion

        [SerializeField] Camera mainCamera;
        [SerializeField] Camera[] houseCameras;

        // INTRODUCTION
        [SerializeField] GameObject introductionCanvas;
        [SerializeField] GameObject introductionTextbox;

        // INSTRUCTIONS
        [SerializeField] GameObject instructionCanvas;
        [SerializeField] GameObject instructionIcon;
        [SerializeField] GameObject instructionPopup;
        private bool instructionPopupShown;

        // SOLAR GAME
        [SerializeField] GameObject mainCanvas;
        [SerializeField] GameObject solarGameCanvas;
        [SerializeField] GameObject energyBar;
        [SerializeField] GameObject budget;
        [SerializeField] GameObject compass;
        [SerializeField] GameObject doneButton;
        [SerializeField] GameObject arrowInstructions;

        [SerializeField] GameObject firstHouse;
        private bool firstHousePopUpShown;

        // HOUSES 
        public int housesLeft = 3;
        [SerializeField] GameObject housesLeftUI;
        [SerializeField] TextMeshProUGUI housesLeftText;

        // ENDING
        [SerializeField] GameObject endCamera;
        [SerializeField] GameObject endButton;
        [SerializeField] GameObject endCanvas;
        [SerializeField] GameObject continueBtn;
        [SerializeField] TextMeshProUGUI endingText;
        [SerializeField] GameObject endingTextbox;
        [SerializeField] GameObject levelLoader;
        [SerializeField] GameObject proceedButton;
        [SerializeField] GameObject highScore;
        [SerializeField] TextMeshProUGUI highScoreText;

        private float finalEnergyScore;

        int endResult;

        public enum GameState
        {
            Introduction,
            SelectHouse,
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


        // Start is called before the first frame update
        void Start()
        {
            QuestInitialSetUp();

            // !!EDIT THIS LINE TO CHANGE STARTING STATE FOR DEBUGGING PURPOSES!!
            if(GlobalControl.Instance.solarQuestPlayed || GlobalControl.Instance.solarIntroPlayed){
                CurrentState = GameState.SelectHouse;
            }
            else{
			CurrentState = GameState.Introduction;
            }
        }

        private void QuestInitialSetUp()
        {
            // SET UP OF CAMERAS
            foreach (Camera c in houseCameras)
            {
                c.enabled = false;
                c.gameObject.SetActive(false);
            }
            mainCamera.enabled = true;
            mainCamera.gameObject.SetActive(true);
            mainCamera.GetComponent<PanZoom>().movementEnabled = false;
            endCamera.SetActive(false);

            // INTRODUCTION
            introductionCanvas.SetActive(false);

            // INSTRUCTIONS
            instructionCanvas.SetActive(false);
            instructionIcon.SetActive(false);
            instructionPopup.SetActive(false);

            // UI
            doneButton.SetActive(false);
            housesLeftUI.SetActive(false);

            // SOLAR GAME
            mainCanvas.SetActive(false);
            solarGameCanvas.SetActive(false);
            arrowInstructions.SetActive(false);
            firstHouse.SetActive(false);

            // END
            endButton.SetActive(false);
            endCanvas.SetActive(false);
            highScore.SetActive(false);
        }

        private void SetState(GameState newState)
        {

            switch (newState)
            {
                case GameState.Introduction:
                    HandleIntroductionState_On();
                    break;
                case GameState.SelectHouse:
                    HandleSelectHouseState_On();
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
                case GameState.SelectHouse:
                    HandleSelectHouseState_Off();
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
            introductionCanvas.SetActive(true);
            introductionTextbox.GetComponent<BlockSceneIntroBox>().LoadText();
            GlobalControl.Instance.solarIntroPlayed = true;
        }
        private void HandleIntroductionState_Off()
        {
            introductionCanvas.SetActive(false);
        }
        #endregion

        #region SelectHouseState
        private void HandleSelectHouseState_On()
        {
            mainCanvas.SetActive(true);
            solarGameCanvas.SetActive(true);

            // Set map view so that houses can be selected
            GetComponent<HouseSelector>().MapView = true;

            // Display initial instruction screen
            instructionCanvas.SetActive(true);
            instructionIcon.SetActive(true);

            // Set up houses left ui
            housesLeftUI.SetActive(true);
            housesLeftText.text = "Houses Left: " + housesLeft;
        }

        private void HandleSelectHouseState_Off()
        {
            instructionCanvas.SetActive(false);
            mainCanvas.SetActive(false);
            solarGameCanvas.SetActive(false);

            // Set map view so that houses can be selected
            GetComponent<HouseSelector>().MapView = false;

            finalEnergyScore = SolarScoring.Instance.energyScore;
        }

        // Show and hide instruction screen
        public void ShowInstructions()
        {
            instructionCanvas.SetActive(true);
        }

        public void HideInstructions()
        {
            /*
            if (!instructionPopupShown)
            {
                instructionPopupShown = true;
                instructionPopup.SetActive(true);
            }*/
            instructionCanvas.SetActive(false);
        }

        public void CloseInstructionPopup()
        {
            instructionPopup.SetActive(false);
        }
        public void CloseArrowInstructions()
        {
            arrowInstructions.SetActive(false);
        }

        private void DecrementHousesLeft()
        {
            housesLeft--;
            Mathf.Clamp(housesLeft, 0, 3);
            housesLeftText.text = "Houses Left: " + housesLeft;
        }

        IEnumerator FirstHousePopUp()
        {
            firstHouse.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            firstHouse.SetActive(false);
        }
        #endregion

        #region EndState

        [SerializeField] GameObject energyVis;
        [SerializeField] GameObject energyVis_lightbulb;
        [SerializeField] GameObject energyVis_fridgetv;
        [SerializeField] GameObject energyVis_heatpump;
        [SerializeField] GameObject energyVis_car;
        [SerializeField] GameObject energyVis_max;
        [SerializeField] GameObject energyVis_zero;

        private void HandleEndState_On()
        {
            mainCamera.gameObject.SetActive(false);
            endCamera.SetActive(true);
            endCamera.GetComponent<EndCamera>().ZoomOutCamera();
        }

        public void ProceedToEndText()
        {
            proceedButton.SetActive(false);
            endCanvas.SetActive(true);
            endingText.text = GetEndOutcome();
            DisplayEnergyVisualization();
            Debug.Log(finalEnergyScore);
        }

        private void HandleEndState_Off()
        {
            endCanvas.SetActive(false);
        }

        public void EndGame()
        {
            CurrentState = GameState.End;
        }

        public void TryAgain()
        {
            // Load current scene again
            SceneManager.LoadScene(3);
        }

        public void Continue()
        {
            if (finalEnergyScore > 0.2)
            {
                // Load Kitsilano scene

            }

            //endingTextbox.GetComponent<BlockSceneEndTextbox>().LoadText();
            endingTextbox.GetComponent<BlockSceneEndTextbox>().SetEnding(endResult);
        }

        private string GetEndOutcome()
        {
            int roundedScore = 0;
            roundedScore = Mathf.RoundToInt(finalEnergyScore*100);

            string outcome = "";
            if(finalEnergyScore < 0.2)
            {
                outcome = "<b>Oops! You must play again.</b>";
                
                continueBtn.SetActive(false);
            
            }
            else if (finalEnergyScore <= 0.75)
            {
                outcome = "<b>Could do better!</b>";                
                continueBtn.SetActive(true);
            }
            else if (finalEnergyScore <= 0.95)
            {
                outcome = "<b>You’re almost there!</b>";                
                continueBtn.SetActive(true);
            }
            else if (finalEnergyScore < 1)
            {
                outcome = "<b>You did great!</b>"; 
                continueBtn.SetActive(true);
            }
            else 
            {
                outcome = "<b>Wow! You’re a Champion!</b>";
                continueBtn.SetActive(true);
            }

            outcome += " You achieved " + roundedScore.ToString() +"% of the total solar potential for the street.";
            // SET THE HIGH SCORE

            if(  GlobalControl.Instance.solarQuestHighScore < roundedScore)
            {
                GlobalControl.Instance.solarQuestHighScore = roundedScore;
				PlayerPrefs.SetInt("SolarScore", roundedScore);
                highScoreText.text = roundedScore.ToString() + "%";
                highScore.SetActive(true);
            }

            return outcome;
        }

        private void DisplayEnergyVisualization()
        {
            energyVis.SetActive(true);
            if (finalEnergyScore <= 0)
            {
                energyVis_zero.SetActive(true);
                endResult = 0;
            }
            else if (finalEnergyScore < 0.03)
            {
                energyVis_lightbulb.SetActive(true);
                endResult = 1;
            }
            else if (finalEnergyScore < 0.2)
            {
                energyVis_fridgetv.SetActive(true);
                endResult = 2;
            }
            else if (finalEnergyScore < 0.75)
            {
                energyVis_heatpump.SetActive(true);
                endResult = 3;
            }
            else if (finalEnergyScore < 1)
            {
                energyVis_car.SetActive(true);
                endResult = 4;
            }
            else
            {
                energyVis_max.SetActive(true);
                endResult = 5;
            }
        }
        #endregion

        // Going from block camera to house camera
        public void UseCamera(int cameraIndex)
        {
            if (cameraIndex >= houseCameras.Length)
            {
                Debug.Log("No such camera");
                return;
            }

            for (int i = 0; i < houseCameras.Length; i++)
            {
                if (i == cameraIndex)
                {
                    houseCameras[i].enabled = true;
                    houseCameras[i].gameObject.SetActive(true);
                }
                else
                {
                    houseCameras[i].enabled = false;
                    houseCameras[i].gameObject.SetActive(false);
                }
            }

            mainCamera.enabled = false;
            mainCamera.gameObject.SetActive(false);

            instructionIcon.SetActive(false);
            arrowInstructions.SetActive(true);
            endButton.SetActive(false);

            GetComponent<HouseSelector>().MapView = false;
        }


        // Going back from house camera to block camera
        public void ShowMap()
        {
            // Encouragement for finishing one house
            if (housesLeft == 2 && !firstHousePopUpShown)
            {
                StartCoroutine("FirstHousePopUp");
                firstHousePopUpShown = true;
            }

            arrowInstructions.SetActive(false);
            GetComponent<HouseSelector>().SwitchToMapView();
            instructionIcon.SetActive(true);
            doneButton.SetActive(false);

            // Switch back to block camera
            mainCamera.enabled = true;
            mainCamera.gameObject.SetActive(true);
            foreach (Camera c in houseCameras)
            {
                c.gameObject.SetActive(false);
                c.enabled = false;
            }


            // Enable end quest button
            if (housesLeft <= 0)
            {
                endButton.SetActive(true);
            }
        }













    }

}
