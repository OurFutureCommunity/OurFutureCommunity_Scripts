using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class GridManager : MonoBehaviour
    {
        public GameObject[] grids;

        public GridGenerator[] gridGenerators;

        private void Start()
        {
            gridGenerators = new GridGenerator[grids.Length];
            for (int i = 0; i < grids.Length; i++)
            {
                gridGenerators[i] = grids[i].GetComponent<GridGenerator>();
            }
        }

        public GameObject GetNearestGrid(Vector3 panelPos)
        {
            GameObject closestGrid = grids[0];
            float minDist = Mathf.Infinity;
            foreach (GameObject g in grids)
            {
                float dist = Vector3.Distance(g.transform.position, panelPos);
                if (dist < minDist)
                {
                    closestGrid = g;
                    minDist = dist;
                }
            }

            return closestGrid;
        }
    }
}

