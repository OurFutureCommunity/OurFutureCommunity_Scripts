using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public abstract class GridGenerator : MonoBehaviour
    {

        [SerializeField] GameObject gridCubePrefab;
        [SerializeField] protected int row = 5;
        [SerializeField] protected int col = 5;
        [SerializeField] protected GameObject solarGame;

        Vector3 pos;
        Vector3 newPos;
        Vector3[,] posArray;
        protected int[,] occupied;
        float space = 1.5f;

        protected float size;
        protected float gridScore;


        void Awake()
        {
            posArray = new Vector3[row, col];
            occupied = new int[row + 2, col + 2];
            size = row * col;
            gridScore = 0f;
        }


        // Use this for initialization
        void Start()
        {
            MakePuzzle();
            pos = transform.position;
        }

        public float GridScore
        {
            get { return gridScore; }
        }

        public float GridSize
        {
            get { return size; }
        }

        void MakePuzzle()
        {
            pos = gameObject.transform.position;
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    GameObject gridCube = Instantiate(gridCubePrefab, pos, transform.rotation);
                    gridCube.transform.parent = transform;
                    gridCube.transform.localPosition = new Vector3(0, 0, 0);
                    gridCube.transform.localRotation = Quaternion.identity;

                    pos = gridCube.transform.localPosition;
                    newPos = new Vector3(pos.x + space * c, pos.y + space * r, pos.z);
                    gridCube.transform.localPosition = newPos;

                    posArray[r, c] = gridCube.transform.position;
                    occupied[r + 1, c + 1] = 0;
                }
            }
        }

        public int[] GetGridPos(Vector3 position)
        {
            float smallestDistance = 2f;
            int[] result = new int[2];
            result[0] = -1;
            result[1] = -1;

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    float distance = Vector3.Distance(position, posArray[r, c]);
                    if (distance < smallestDistance)
                    {
                        result[0] = r;
                        result[1] = c;
                        smallestDistance = distance;
                    }
                }
            }

            return result;
        }

        public Vector3 GetNearestPointOnGrid(int row, int col)
        {
            if (row >= 0)
            {
                return posArray[row, col];
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }

        public void UpdateOccupiedPositions(int r, int c, string panelCost)
        {
            if (r >= 0)
            {
                r++;
                c++;
                // Sets the occupied grid array to know which squares has a solar panel on it
                if (panelCost == "12000")
                {
                    occupied[r - 1, c - 1]++;
                    occupied[r - 1, c]++;
                    occupied[r - 1, c + 1]++;
                    occupied[r, c - 1]++;
                    occupied[r, c]++;
                    occupied[r, c + 1]++;
                    occupied[r + 1, c - 1]++;
                    occupied[r + 1, c]++;
                    occupied[r + 1, c + 1]++;
                }
                else if (panelCost == "3000")
                {
                    occupied[r, c]++;
                }
            }
        }

        public void ClearPanelOccupancy(int r, int c, string panelCost)
        {
            r++;
            c++;
            if (panelCost == "12000")
            {
                occupied[r - 1, c - 1]--;
                occupied[r - 1, c]--;
                occupied[r - 1, c + 1]--;
                occupied[r, c - 1]--;
                occupied[r, c]--;
                occupied[r, c + 1]--;
                occupied[r + 1, c - 1]--;
                occupied[r + 1, c]--;
                occupied[r + 1, c + 1]--;
            }
            else if (panelCost == "3000")
            {
                occupied[r, c]--;
            }
        }

        public abstract void UpdateGridScore();

    }
}
