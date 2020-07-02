using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class SolarScoring : MonoBehaviour
    {
        public float energyScore;
        public GameObject energyBar;

        [SerializeField] List<GameObject> solarGamesObjects;
        [SerializeField] float bestScore;

        private List<SolarGame> solarGames;
        private bool firstTimeSetUp = false;

        [SerializeField] float energyBarFillHeight;
        private float scoreDiff;
        public GameObject incrementPrefab;
        // Used as a parent to instantiate increments in 
        public GameObject energyBarBorder;
        private GameObject increment;



        #region Singleton
        public static SolarScoring Instance;

        private void Awake()
        {
            Instance = this;
            energyScore = 0f;
        }
        #endregion Singleton


        private void Start()
        {
            increment = Instantiate(incrementPrefab, energyBar.transform.position, Quaternion.identity) as GameObject;
            increment.transform.SetParent(energyBarBorder.transform);
            InitializeEnergyBar();
        }

        public void InitializeEnergyBar()
        {
            energyScore = 0;
            energyBar.transform.localScale = new Vector3(1, 0, 1);            
            increment.transform.localScale = new Vector3(1, 0, 1);
        }

        public void UpdateEnergyBar()
        {
            if (!firstTimeSetUp)
            {
                LoadSolarGames();
            }            

            // Calculate the new score
            float newScore = 0;
            foreach (SolarGame sg in solarGames)
            {
                newScore += sg.houseScore;
            }

            newScore /= bestScore;
            scoreDiff = newScore - energyScore;
            

            increment.transform.position = energyBar.transform.position + new Vector3(0, energyBarFillHeight * energyScore, 0); 
            if (scoreDiff >= 0)
            {
                increment.transform.localScale = new Vector3(1, scoreDiff, 1);
            }
            else
            {
                increment.transform.localScale = new Vector3(1, 0, 1);
            }
            energyScore = newScore;
            energyBar.transform.localScale = new Vector3(1, energyScore, 1);


        }

        private void LoadSolarGames()
        {
            solarGames = new List<SolarGame>();
            foreach (GameObject g in solarGamesObjects)
            {
                solarGames.Add(g.GetComponent<SolarGame>());
            }
            firstTimeSetUp = true;
        }
    }
}

