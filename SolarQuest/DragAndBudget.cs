using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SolarQuest
{
    public class DragAndBudget : MonoBehaviour
    {
        Vector3 initialPosition;
        Quaternion initialRotation;

        public float dragDistance = 10.8f; //distance to drag mouse
        float zPosSolar;
        private Vector3 objPos;

        [SerializeField] GameObject gridManagerScript;
        private GridManager gridManager;

        public GameObject panel;

        bool budgetMoreThanZero;

        SolarPanel solarPanel;
        GameObject currentPanel;

        void Start()
        {
            initialPosition = gameObject.transform.position;
            initialRotation = gameObject.transform.rotation;
            gridManager = gridManagerScript.GetComponent<GridManager>();
        }


        void OnMouseDown()
        {
            // Instantiate a new solar panel when it is clicked on
            if (!GetComponent<SolarPanel>().PanelPlaced)
            {
                currentPanel = Instantiate(panel, initialPosition, initialRotation);
            }
            else
            {
                currentPanel = gameObject;
            }
        }

        void OnMouseDrag()
        {
            if (currentPanel)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dragDistance);
                objPos = Camera.main.ScreenToWorldPoint(mousePosition);
                currentPanel.transform.position = objPos;
            }
        }



        void OnMouseUp()
        {
            string panelCost = currentPanel.tag;
            // budgetMoreThanZero true if there is enough budget to place a panel
            budgetMoreThanZero = BudgetSystem.Instance.ifBudgetNotZero(panelCost);

            // grid is the nearest grid to the solar panel
            GameObject nearestGrid = gridManager.GetNearestGrid(currentPanel.transform.position);
            GridGenerator grid = nearestGrid.GetComponent<GridGenerator>();

            // panel fits on grid
            bool panelFits = PanelFits(panelCost, grid);

            //Find the closest Vector3 of the grid, if it returns (-1,-1) -> nothing is close
            int[] gridPos = grid.GetGridPos(currentPanel.transform.position);

            // Solar panel script on the panel
            solarPanel = currentPanel.GetComponent<SolarPanel>();

            // Return panel to the pile if the location placed is not near grid
            if (gridPos[0] < 0)
            {
                // Only if panel has been placed will the budget be incremented
                if (solarPanel.PanelPlaced)
                {
                    BudgetSystem.Instance.IncrementBudget(panelCost);

                    solarPanel.Grid.ClearPanelOccupancy(solarPanel.gridRow, solarPanel.gridCol, panelCost);
                    solarPanel.Grid.UpdateGridScore();

                    solarPanel.Grid = grid;
                }

                Destroy(currentPanel);
            }
            // Place panel on grid if there is enough budget or panel has been placed before AND panel must fit
            else if ((budgetMoreThanZero || solarPanel.PanelPlaced) && panelFits)
            {
                currentPanel.transform.position = grid.GetNearestPointOnGrid(gridPos[0], gridPos[1]);

                // If panel hasn't been placed before (newly placed)
                if (!solarPanel.PanelPlaced)
                {
                    if (SolarGamePopupManager.Instance != null)
                    {
                        SolarGamePopupManager.Instance.FirstPanelPlaced();
                    }

                    grid.UpdateOccupiedPositions(gridPos[0], gridPos[1], panelCost);

                    StartCoroutine(BudgetSystem.Instance.DecrementBudget(panelCost));
                    currentPanel.GetComponent<SolarPanel>().PanelPlaced = true;
                }
                // Panel is only moved to a different palce
                else
                {
                    // If panel is moved to a different grid 
                    if (solarPanel.Grid && solarPanel.Grid != grid)
                    {
                        // Clear occupancy of previous grid and update score
                        solarPanel.Grid.ClearPanelOccupancy(solarPanel.gridRow, solarPanel.gridCol, panelCost);
                        solarPanel.Grid.UpdateGridScore();
                        grid.UpdateOccupiedPositions(gridPos[0], gridPos[1], panelCost);
                    }
                    else
                    {
                        grid.ClearPanelOccupancy(solarPanel.gridRow, solarPanel.gridCol, panelCost);
                        grid.UpdateOccupiedPositions(gridPos[0], gridPos[1], panelCost);
                    }
                }

                solarPanel.gridRow = gridPos[0];
                solarPanel.gridCol = gridPos[1];

                solarPanel.Grid = grid;
                grid.UpdateGridScore();
            }
            else if (!budgetMoreThanZero)
            {
                BudgetSystem.Instance.OutOfBudget();
                Destroy(currentPanel);
            }
            else
            {
                Destroy(currentPanel);
            }

        }

        private bool PanelFits(string panelCost, GridGenerator grid)
        {
            if (panelCost == "12000")
            {
                if (grid.GridSize < 9)
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
                return true;
            }
        }
    }
}

