using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SolarQuest
{
    public class SolarGame : MonoBehaviour
    {

        public GameObject[] gridObjects;

        private GridManager[] gridManagers;
        private List<GridGenerator> grids;

        public float houseScore;

        private bool initialSetUp;


        void Start()
        {
        }

        public void UpdateScore()
        {
            if (!initialSetUp)
            {
                InitialSetUp();
            }

            float score = 0f;

            foreach (GridGenerator g in grids)
            {
                score += g.GridScore;
            }

            houseScore = score;

            SolarScoring.Instance.UpdateEnergyBar();
        }

        private void InitialSetUp()
        {
            gridManagers = new GridManager[gridObjects.Length];

            for (int i = 0; i < gridManagers.Length; i++)
            {
                gridManagers[i] = gridObjects[i].GetComponent<GridManager>();
            }

            grids = new List<GridGenerator>();
            foreach (GridManager gm in gridManagers)
            {
                foreach (GridGenerator gg in gm.gridGenerators)
                {
                    grids.Add(gg);
                }
            }
            initialSetUp = true;
        }

        public void ResetSolarGame()
        {
            SceneManager.LoadScene(2);
        }

    }
}

