using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
	public class UFTutorialUIPopUpManager : MonoBehaviour
	{
		[SerializeField] GameObject SquirrelStartPos;
		[SerializeField] GameObject SquirrelEndPos;
		[SerializeField] GameObject SquirrelNutzPos;

		[SerializeField] GameObject ControlUI;

		[SerializeField] GameObject BudgetPopup;
		[SerializeField] GameObject BudgetUI;

		[SerializeField] GameObject TutorialCamera;
		[SerializeField] GameObject CameraController;

		float panSpeed = 1f;
		float tutorialCamXEndPos = 70f;


		private static UFTutorialUIPopUpManager instance = null;
		public static UFTutorialUIPopUpManager GetInstance()
		{
			return instance;
		}

		private void Awake()
		{
			instance = this;
		}
		void SetPopupsInactive()
		{
			ControlUI.SetActive(false);
			SquirrelStartPos.SetActive(false);
			SquirrelEndPos.SetActive(false);
			SquirrelNutzPos.SetActive(false);
			BudgetUI.SetActive(false);
			BudgetPopup.SetActive(false);
			
		}

		public void StartTutorial(){
			SetPopupsInactive();
			ShowControlUI();
		}
		
		public void ShowSquirrelStartPosPopup(){
			SquirrelStartPos.SetActive(true);
		}
		public void CloseSquirrelStartPosPopup(){

			SquirrelStartPos.SetActive(false);
			ShowSquirrelNutzPos();

		}

		public void ShowSquirrelNutzPos()
		{
			SquirrelNutzPos.SetActive(true);
		}

		public void CloseSquirrelNutzPos()
		{
			SquirrelNutzPos.SetActive(false);
			StartCoroutine(PanCamToEndSquirrel());
		}

		IEnumerator PanCamToEndSquirrel(){
			while(TutorialCamera.transform.position.x < tutorialCamXEndPos){
				TutorialCamera.transform.position = new Vector3(TutorialCamera.transform.position.x + panSpeed, 
					TutorialCamera.transform.position.y , TutorialCamera.transform.position.z);
				yield return null; 
			}
			SquirrelEndPos.SetActive(true);
		}

		public void CloseSquirrelEndPosPopup(){
			SquirrelEndPos.SetActive(false);
			TutorialCamera.SetActive(false);
			CameraController.SetActive(true);
			ShowControlUI();
		}

		public void ShowControlUI(){
			//CameraController.SetActive(true);
			ControlUI.SetActive(true);
			//Debug.Log("showing controller");
		}

		public void CloseControlUI(){
			ControlUI.SetActive(false);
			ShowBudgetUIPopup();
		}

		public void ShowBudgetUIPopup(){
			BudgetUI.SetActive(true);
			BudgetPopup.SetActive(true);
		}

		public void CloseBudgetUIPopup(){
			BudgetUI.SetActive(false);
			BudgetPopup.SetActive(false);
			UFBlockManager.GetInstance().CurrentState = UFBlockManager.GameState.PlantTrees;
		}
	
	}
}
