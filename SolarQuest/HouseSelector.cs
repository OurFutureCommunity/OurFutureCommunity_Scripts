using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class HouseSelector : MonoBehaviour
    {

        private int houseIndex;
        private GameObject selectedHouse;
        private bool mapView = false;

        public delegate void HouseDelegate();
        public HouseDelegate oHouseSelected;

        [SerializeField] Animator selectBoxAnimator;
        [SerializeField] GameObject noHousesLeft;


        private void Start()
        {
            noHousesLeft.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && mapView)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if (hit.transform && hit.transform.CompareTag("House"))
                    {
                        // Center camera on house and display popup house info and select house button
                        CameraCenter(hit.transform.gameObject);
                        selectBoxAnimator.SetBool("IsOnScreen", true);
                    }
                }
            }
        }

        public bool MapView
        {
            get { return mapView; }
            set { mapView = value; }
        }

        public void SwitchToMapView()
        {
            mapView = true;
            selectedHouse.GetComponent<Collider>().enabled = true;
            selectedHouse.GetComponent<SolarHouse>().HideArrowCanvas();

            // Display the selected house indicator underneath the house
            selectedHouse.GetComponent<SolarHouse>().selectedHouseIndicator.SetActive(true);
        }

        void CameraCenter(GameObject house)
        {
            selectedHouse = house;
            switch (house.name)
            {
                case "House0":
                    //Camera.main.transform.position = new Vector3(-34f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 0;
                    break;
                case "House1":
                    //Camera.main.transform.position = new Vector3(-30f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 1;
                    break;
                case "House2":
                    //Camera.main.transform.position = new Vector3(1f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 2;
                    break;
                case "House3":
                    //Camera.main.transform.position = new Vector3(27f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 3;
                    break;
                case "House4":
                    //Camera.main.transform.position = new Vector3(58f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 4;
                    break;
                case "House5":
                    //Camera.main.transform.position = new Vector3(62f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                    houseIndex = 5;
                    break;
                default:
                    break;
            }
        }

        public void SelectHouse()
        {
            // Hide select box
            selectBoxAnimator.SetBool("IsOnScreen", false);

            SolarHouse solarHouse = selectedHouse.GetComponent<SolarHouse>();

            // Switch cameras
            if (GetComponent<BlockSceneManager>().housesLeft > 0 || solarHouse.Selected)
            {
                GetComponent<BlockSceneManager>().UseCamera(houseIndex);

                // If house hasn't been selected before
                if (!solarHouse.Selected)
                {
                    // Decrement number of houses that can be selected 
                    oHouseSelected();
                }

                // Disable collider so panels can be moved
                selectedHouse.GetComponent<Collider>().enabled = false;

                // Switch to select screen
                solarHouse.SelectRoofScreen();

                solarHouse.selectedHouseIndicator.SetActive(false);
            }
            else
            {
                NoHousesLeft();
            }

        }

        private void NoHousesLeft()
        {
            noHousesLeft.SetActive(true);
        }

        public void HideWarningMessage()
        {
            noHousesLeft.SetActive(false);
        }

        public void CloseSelectHousePrompt()
        {
            selectBoxAnimator.SetBool("IsOnScreen", false);
        }
    }

}
