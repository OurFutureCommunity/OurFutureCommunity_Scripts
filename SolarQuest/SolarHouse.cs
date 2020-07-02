using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class SolarHouse : MonoBehaviour
    {
        [SerializeField] GameObject arrowCanvas;

        [SerializeField] GameObject northCam;
        [SerializeField] GameObject eastCam;
        [SerializeField] GameObject southCam;
        [SerializeField] GameObject westCam;

        [SerializeField] GameObject northSolar;
        [SerializeField] GameObject eastSolar;
        [SerializeField] GameObject southSolar;
        [SerializeField] GameObject westSolar;

        [SerializeField] GameObject backButton;
        [SerializeField] GameObject doneButton;

        [SerializeField] GameObject compass;

        public GameObject selectedHouseIndicator;


        private bool selected;

        public bool Selected
        {
            get { return selected; }
        }

        private void Start()
        {
            northCam.SetActive(false);
            if (eastCam) { eastCam.SetActive(false); }
            if (westCam) { westCam.SetActive(false); }

            northSolar.SetActive(false);
            southSolar.SetActive(false);

            if (eastSolar) { eastSolar.SetActive(false); }
            if (westSolar) { westSolar.SetActive(false); }

            backButton.SetActive(false);
            doneButton.SetActive(false);

            arrowCanvas.SetActive(false);
            selected = false;

            selectedHouseIndicator.SetActive(false);
        }

        public void SelectRoofScreen()
        {
            selected = true;
            arrowCanvas.SetActive(true);
            compass.SetActive(true);
            doneButton.SetActive(true);
            backButton.SetActive(false);

            northCam.SetActive(false);
            southCam.SetActive(true);

            if (eastCam) { eastCam.SetActive(false); }
            if (westCam) { westCam.SetActive(false); }

            northSolar.SetActive(false);
            southSolar.SetActive(false);

            if (eastSolar) { eastSolar.SetActive(false); }
            if (westSolar) { westSolar.SetActive(false); }
        }


        public void SwitchCameraToSouth()
        {
            SwitchCameraSetup();

            southSolar.SetActive(true);
        }

        public void SwitchCameraToNorth()
        {
            SwitchCameraSetup();

            northSolar.SetActive(true);


            northCam.SetActive(true);
            southCam.SetActive(false);

            if (eastCam) { eastCam.SetActive(false); }
            if (westCam) { westCam.SetActive(false); }

        }

        public void SwitchCameraToEast()
        {
            SwitchCameraSetup();

            eastSolar.SetActive(true);

            northCam.SetActive(false);
            eastCam.SetActive(true);
            southCam.SetActive(false);

            if (westCam) { westCam.SetActive(false); }
        }

        public void SwitchCameraToWest()
        {
            SwitchCameraSetup();

            westSolar.SetActive(true);

            northCam.SetActive(false);
            westCam.SetActive(true);
            southCam.SetActive(false);

            if (eastCam) { eastCam.SetActive(false); }
        }

        private void SwitchCameraSetup()
        {
            BlockSceneManager.GetInstance().CloseArrowInstructions();
            arrowCanvas.SetActive(false);
            backButton.SetActive(true);
            compass.SetActive(false);
            doneButton.SetActive(false);
        }

        public void HideArrowCanvas()
        {
            arrowCanvas.SetActive(false);
        }


    }
}

