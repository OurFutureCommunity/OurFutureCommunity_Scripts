using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class SolarGamePopupManager : MonoBehaviour
    {

        #region Singleton
        public static SolarGamePopupManager Instance;

        private void Awake()
        {
            Instance = this;
        }
        #endregion Singleton

        private bool firstPanelPlaced;

        [SerializeField] GameObject encouragementMessage;
        [SerializeField] GameObject budgetReminderPopup;
        [SerializeField] GameObject energyReminderPopup;


        private void Start()
        {
            firstPanelPlaced = false;
            encouragementMessage.SetActive(false);
            budgetReminderPopup.SetActive(false);
        }

        public void FirstPanelPlaced()
        {
            if (!firstPanelPlaced)
            {
                StartCoroutine("PanelPlacedSequence");
            }
            firstPanelPlaced = true;
        }

        IEnumerator PanelPlacedSequence()
        {
            encouragementMessage.SetActive(true);
            yield return new WaitForSeconds(3f);
            encouragementMessage.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            budgetReminderPopup.SetActive(true);
        }

        public void CloseBudgetReminderPopUp()
        {
            budgetReminderPopup.SetActive(false);
            energyReminderPopup.SetActive(true);
        }

        public void CloseEnergyReminderPopUp()
        {
            energyReminderPopup.SetActive(false);
        }

        public void CloseAllPopups()
        {
            encouragementMessage.SetActive(false);
            budgetReminderPopup.SetActive(false);
            energyReminderPopup.SetActive(false);
        }
    }
}

