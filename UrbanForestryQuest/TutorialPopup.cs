using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class TutorialPopup : MonoBehaviour
    {
        // BUTTONS
        [SerializeField] GameObject plantTreeButton;
		[SerializeField] GameObject plantFlowerButton;
		[SerializeField] GameObject plantShadeTreeButton;
        [SerializeField] GameObject deleteTreeButton;
        [SerializeField] GameObject budgetText;
        [SerializeField] GameObject canopyCoverBar;
        //[SerializeField] GameObject switchCameraButton;
        [SerializeField] GameObject doneButton;

        // POPUPS
        [SerializeField] GameObject controlPopup;
        [SerializeField] GameObject budgetPopup;
        [SerializeField] GameObject canopyCoverPopup;
        [SerializeField] GameObject plantTreePopup;
        [SerializeField] GameObject greenBoxpopup;
		[SerializeField] GameObject plantFlowerPopup;
        [SerializeField] GameObject budgetReminderPopup;
        [SerializeField] GameObject canopyCoverReminderPopup;
        [SerializeField] GameObject deleteTreePopup;
        //[SerializeField] GameObject switchCameraPopup;
        [SerializeField] GameObject donePopup;

		// Squirrels 
		[SerializeField] GameObject squirrelStartPopup;
		[SerializeField] GameObject squirrelEndPopup;
		[SerializeField] GameObject squirrelGoalPopup;

        private bool firstTreePlanted;
		private bool secondTreePlanted;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            if (!firstTreePlanted && LevelManager.GetInstance().inSceneGameObjects.Count > 0)
            {
                ShowGreenBoxPopup();
                firstTreePlanted = true;
            }
			if (!secondTreePlanted && firstTreePlanted && LevelManager.GetInstance().inSceneGameObjects.Count >1)
			{
				//ShowDeleteTreePopup();
                ShowDonePopup();
				secondTreePlanted = true;
			}
        }

        public void InitializeTutorial()
        {
            plantTreeButton.SetActive(false);
			plantFlowerButton.SetActive(false);
			plantShadeTreeButton.SetActive(false);
            deleteTreeButton.SetActive(false);
            budgetText.SetActive(false);
            canopyCoverBar.SetActive(false);
            //switchCameraButton.SetActive(false);
            doneButton.SetActive(false);

			SetPopupsInactive();

			//ShowBudgetPopup();
			//ShowControlPopup();
			ShowCanopyCoverPopup();
        }

		void SetPopupsInactive(){
			controlPopup.SetActive(false);
			budgetPopup.SetActive(false);
			canopyCoverPopup.SetActive(false);
			plantTreePopup.SetActive(false);
			greenBoxpopup.SetActive(false);
			plantFlowerPopup.SetActive(false);
			budgetReminderPopup.SetActive(false);
			canopyCoverReminderPopup.SetActive(false);
			deleteTreePopup.SetActive(false);
			//switchCameraPopup.SetActive(false);
			donePopup.SetActive(false);

			squirrelStartPopup.SetActive(false);
			squirrelEndPopup.SetActive(false);
			squirrelGoalPopup.SetActive(false);


		}

        public void ShowControlPopup()
        {
            controlPopup.SetActive(true);
        }

        public void ShowBudgetPopup()
        {
			SetPopupsInactive();

            budgetText.SetActive(true);
            budgetPopup.SetActive(true);
        }

        public void ShowCanopyCoverPopup()
        {
			SetPopupsInactive();

            canopyCoverBar.SetActive(true);
            canopyCoverPopup.SetActive(true);
        }

		public void ShowSquirrelGoalPopup()
		{
			SetPopupsInactive();

			squirrelGoalPopup.SetActive(true);
            squirrelStartPopup.SetActive(true);
            squirrelEndPopup.SetActive(true);
        }
        /*
		public void ShowSquirrelStartPopup()
		{
			SetPopupsInactive();

			squirrelStartPopup.SetActive(true);

		}
		public void ShowSquirrelEndPopup()
		{
			SetPopupsInactive();

			squirrelEndPopup.SetActive(true);

		}*/
        public void ShowPlantTreePopup()
        {
			SetPopupsInactive();

            plantTreeButton.SetActive(true);
			plantFlowerButton.SetActive(false);
			plantShadeTreeButton.SetActive(true);
			deleteTreeButton.SetActive(false);
            plantTreePopup.SetActive(true);
        }    

        public void ClosePlantTreePopup()
        {
            plantTreePopup.SetActive(false);
        }
        public void ShowGreenBoxPopup()
        {
            greenBoxpopup.SetActive(true);
        }

        public void CloseGreenBoxpopup()
        {
            greenBoxpopup.SetActive(false);
        }
        private IEnumerator FirstTreePlantedPopup()
        {
            greenBoxpopup.SetActive(true);
            yield return new WaitForSeconds(3f);
            greenBoxpopup.SetActive(false);
            //yield return new WaitForSeconds(0.5f);

            //deleteTreeButton.SetActive(true);
            //deleteTreePopup.SetActive(true);

			ShowFlowerTreePopup();
        }

		public void ShowFlowerTreePopup()
		{	
			//SetPopupsInactive();

			plantFlowerButton.SetActive(true);
			plantFlowerPopup.SetActive(true);
		}

		public void CloseFlowerTreePopup()
		{
			plantFlowerPopup.SetActive(false);
		}

		public void ShowDeleteTreePopup()
		{
			//SetPopupsInactive();

			//deleteTreeButton.SetActive(true);
			deleteTreePopup.SetActive(true);
		}

		public void CloseDeleteTreePopup()
		{
			//SetPopupsInactive();

			//deleteTreeButton.SetActive(true);
			deleteTreePopup.SetActive(false);
		}


        public void ShowBudgetReminderPopup()
        {
            budgetReminderPopup.SetActive(true);
            deleteTreePopup.SetActive(false);
        }

        public void ShowCanopyCoverReminderPopup()
        {
            budgetReminderPopup.SetActive(false);
            canopyCoverReminderPopup.SetActive(true);
        }

        public void ShowSwitchCameraPopup()
        {
            controlPopup.SetActive(false);

            //switchCameraButton.SetActive(true);
            //switchCameraPopup.SetActive(true);
        }

        public void ShowDonePopup()
        {
			SetPopupsInactive();

            donePopup.SetActive(true);
            doneButton.SetActive(true);
        }

        public void CloseDonePopup()
        {
			SetPopupsInactive();

            //plantTreeButton.SetActive(false);
            //deleteTreeButton.SetActive(false);
            //budgetText.SetActive(false);
            //canopyCoverBar.SetActive(false);
            //switchCameraButton.SetActive(false);
            //doneButton.SetActive(false);
        }
    }
}

