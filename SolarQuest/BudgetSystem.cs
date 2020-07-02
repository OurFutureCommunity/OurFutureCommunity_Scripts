using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SolarQuest
{
    public class BudgetSystem : MonoBehaviour
    {
        public TextMeshProUGUI budgetText;
        public TextMeshProUGUI subtractBudgetText;

        [SerializeField] int maxBudget = 90000;
        [SerializeField] GameObject outOfBudget;
        

        int currentBudget;

        #region Singleton
        public static BudgetSystem Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion Singleton



        // Start is called before the first frame update
        void Start()
        {
           InitBudget();

            // Out of budget popup hidden on start 
            outOfBudget.SetActive(false);
        }

        public void InitBudget()
        {
            budgetText.text = "$" + maxBudget;
            currentBudget = maxBudget;
            subtractBudgetText.text = "";
        }

        public IEnumerator DecrementBudget(string tag)
        {
            if (tag == "12000")
            {
                currentBudget = currentBudget - 13500;
                budgetText.text = "$" + currentBudget;
                subtractBudgetText.text = " -$13500";
            }
            else if (tag == "3000")
            {
                currentBudget = currentBudget - 3000;
                budgetText.text = "$" + currentBudget;
                subtractBudgetText.text = " -$3000";
            }

            else
            {
                budgetText.text = "Budget: $" + currentBudget;
            }

            yield return new WaitForSeconds(0.7f);
            subtractBudgetText.text = "";
        }

        public void IncrementBudget(string tag)
        {
            if (tag == "12000")
            {
                currentBudget = currentBudget + 13500;
                budgetText.text = "$" + currentBudget;
                //subtractBudgetText.text = " +$12000";
            }
            else if (tag == "3000")
            {
                currentBudget = currentBudget + 3000;
                budgetText.text = "$" + currentBudget;
                //subtractBudgetText.text = " +$3000";
            }

            else
            {
                budgetText.text = "$" + currentBudget;
            }

            //yield return new WaitForSeconds(0.7f);
            //subtractBudgetText.text = "";
        }

        // Returns false if there is not enough budget for a given price 
        public bool ifBudgetNotZero(string tag)
        {
            if (tag == "12000")
            {
                if ((currentBudget - 13500) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (tag == "3000")
            {
                if ((currentBudget - 3000) < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            else
            {
                return false;
            }

        }

        public void OutOfBudget()
        {
            outOfBudget.SetActive(true);
        }

        public void HideOutOfBudget()
        {
            outOfBudget.SetActive(false);
        }


    }
}
