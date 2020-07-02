using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace UrbanForestryQuest
{
    public class BudgetManager : MonoBehaviour
    {
        [SerializeField] GameObject noMoneyPopup;
        [SerializeField] int maxBudget = 1000;
        [SerializeField] TextMeshProUGUI currentBudgetText;
        [SerializeField] int largeTreePrice = 100;
		[SerializeField] int medTreePrice = 50;

		[SerializeField] TextMeshProUGUI decrementText;
		[SerializeField] TextMeshProUGUI incrementText;

        public int currentBudget;
        public int startBudget;


        #region Singleton
        private static BudgetManager instance = null;
        public static BudgetManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }
        #endregion

        private void Start()
        {
            decrementText.gameObject.SetActive(false);
            incrementText.gameObject.SetActive(false);
            noMoneyPopup.SetActive(false);
            startBudget = maxBudget;
            currentBudget = maxBudget;
            UpdateBudgetText();
        }

        public bool haveEnoughMoney(GameObject tree)
        {
            //Debug.Log((currentBudget - largeTreePrice));
            if (tree.tag == "largeTree")
            {
                if(currentBudget - largeTreePrice < 0)
                {
                    return false;
                }
            }
            else
            {
                if (currentBudget - medTreePrice < 0)
                {
                    //Debug.Log(currentBudget);
                    return false;
                }
            }

            return true;
        }

        public void DecrementBudget(bool largerTree)
        {
			/*
            Debug.Log("DecrementBudget");
            if (currentBudget - largeTreePrice < 0)
            {
                // Display out of budget message
				Debug.Log("Negative Price");
            }
            else
            {*/

			if(largerTree){
	            currentBudget -= largeTreePrice;
	            UpdateBudgetText();
	            StartCoroutine(DecrementBudgetCoroutine(largeTreePrice));
				//Debug.Log("Update Budget Larger Tree");
			}
			else{
				currentBudget -= medTreePrice;
				UpdateBudgetText();
				StartCoroutine(DecrementBudgetCoroutine(medTreePrice));
				//Debug.Log("Update Budget Med Tree");
			}

            //}
        }

        public void IncrementBudget(bool largerTree)
        {
			if(largerTree){
				currentBudget += largeTreePrice;
				UpdateBudgetText();
				StartCoroutine(IncrementBudgetCoroutine(largeTreePrice));
				//Debug.Log("Update Budget Larger Tree");
			}
			else{
				currentBudget += medTreePrice;
				UpdateBudgetText();
				StartCoroutine(IncrementBudgetCoroutine(medTreePrice));
				//Debug.Log("Update Budget Med Tree");
			}


        }

        private void UpdateBudgetText()
        {
            currentBudgetText.text = "$" + currentBudget.ToString();
        }

        private IEnumerator DecrementBudgetCoroutine(int val)
        {
			decrementText.text = "- $" + val.ToString();
            decrementText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
			decrementText.gameObject.SetActive(false);
        }

        private IEnumerator IncrementBudgetCoroutine(int val)
        {
			incrementText.text = "+ $" + val.ToString();
            incrementText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            incrementText.gameObject.SetActive(false);
        }

        public void CloseNoMoreMoneyPopup()
        {
            noMoneyPopup.SetActive(false);
        }

        public void OpenNoMoreMoneyPopup()
        {
            noMoneyPopup.SetActive(true);
        }

        public void ResetBudget()
        {
            Debug.Log("reset budget");
            currentBudget = maxBudget;
            UpdateBudgetText();
        }
    }
}

