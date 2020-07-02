using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class PlantTrees : MonoBehaviour
    {
        LevelManager levelManager;
        GridBase gridBase;
        InterfaceManager ui;
        UISpace uiSpace;
        BudgetManager budgetManger;

		public int largeTreeMultiplier = 2;
		public int medTreeMultiplier = 1;
        bool placeModeOn;
        public bool deleteModeOn;
        bool objMoving;
        GameObject currentObject;
        Node curNode;

        // Place obj variables
        GameObject objToPlace;
        GameObject cloneObj;
        Level_Object objProperties;
        Vector3 mousePosition;
        Vector3 worldPosition;

        Vector3 startPos;

        float minDistance = 0.1f;

        bool isDragging;

        // Tile painting
        bool paintTile;
        public Material matToPlace;
        Material origMaterial;
        int multiplier;
        
        Quaternion targetRot;

        [SerializeField] GameObject placeButton;
        [SerializeField] GameObject deleteButton;

        [SerializeField] GameObject cameraControllerObj;
        CameraController cameraController;

        public GameObject smallTree;

        GameObject currentTree;
        Level_Object currentTreeProperties;

        private void Start()
        {
            gridBase = GridBase.GetInstance();
            levelManager = LevelManager.GetInstance();
            budgetManger = BudgetManager.GetInstance();
            ui = InterfaceManager.GetInstance();
            uiSpace = UISpace.GetInstance();

            cameraController = cameraControllerObj.GetComponent<CameraController>();
        }

        private void Update()
        {
            DeleteObjects();
        }

        void UpdateMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                mousePosition = hit.point;
            }
        }

        public void CloseAllModes()
        {
            if (deleteModeOn)
            {
                DeleteModeToggle();
            }

            if (placeModeOn)
            {
                PlaceModeToggle();
            }
        }

        // Toggle the ability to place trees on/off
        public void PlaceModeToggle()
        {
			Debug.Log("isThisInUSe? ???? ");
            if (deleteModeOn)
            {
                DeleteModeToggle();
            }
            if (!placeModeOn)
            {
                placeModeOn = true;
                placeButton.GetComponent<ButtonToggle>().On = true;
                objToPlace = ResourceManager.GetInstance().GetObjBase("tree_small").objPrefab;

                cameraController.MovementEnabled = false;
            }
            else
            {
				Debug.Log("isThisInUSe?");
                placeModeOn = false;
                placeButton.GetComponent<ButtonToggle>().On = false;

                // If there is a tree placed
                if (cloneObj != null)
                {
                    // Add to scene object list
                    levelManager.inSceneGameObjects.Add(cloneObj);
                    //budgetManger.DecrementBudget();
                    // Update the score
                    levelManager.UpdateCanopyScore();
                    curNode.placedObj = objProperties;
                    cloneObj = null;
                }
                curNode = null;

                cameraController.MovementEnabled = true;
            }
        }

        // Toggle the ability to delete trees on/off
        public void DeleteModeToggle()
        {
            if (placeModeOn)
            {
                PlaceModeToggle();
            }
            if (!deleteModeOn)
            {
                deleteModeOn = true;
                deleteButton.GetComponent<ButtonToggle>().On = true;

                cameraController.MovementEnabled = false;
            }
            else
            {
                deleteModeOn = false;
                deleteButton.GetComponent<ButtonToggle>().On = false;

                cameraController.MovementEnabled = true;
            }
        }

        #region Place Objects
        public void BeginTreeDrag(GameObject treeToPlace)
        {
            // dont plant trees if budget is 0
            if(!budgetManger.haveEnoughMoney(treeToPlace))
            {
                budgetManger.OpenNoMoreMoneyPopup();
                return;
            }

            cameraController.MovementEnabled = false;
            //Debug.Log("Begin drag");
            UpdateMousePosition();
            curNode = gridBase.NodeFromWorldPosition(mousePosition);
            worldPosition = curNode.vis.transform.position;
            startPos = worldPosition;
            // If object hasn't been instantiated then instantiate it
            /*
            if (currentTree == null)
            {
                currentTree = Instantiate(treeToPlace, worldPosition, Quaternion.identity) as GameObject;
                currentTreeProperties = currentTree.GetComponent<Level_Object>();
            }*/
        }

        public void DuringTreeDrag(GameObject treeToPlace)
        {
            if(!budgetManger.haveEnoughMoney(treeToPlace))
            {
                return;
            }
            // If object hasn't been instantiated then instantiate it
            if (currentTree == null)
            {
                currentTree = Instantiate(treeToPlace, worldPosition, Quaternion.identity) as GameObject;
                currentTreeProperties = currentTree.GetComponent<Level_Object>();
            }
            //Debug.Log("dragging");
            /*
            if(currentTree == null)
            {
                return;
            }*/
            UpdateMousePosition();
            curNode = gridBase.NodeFromWorldPosition(mousePosition);
            worldPosition = curNode.vis.transform.position;
            currentTree.transform.position = worldPosition;
            
        }

        public void EndTreeDrag()
        {           
            if (currentTree == null)
            {
                return;
            }
            if(currentTreeProperties.deleteTree){
                Destroy(currentTree);
                return;
            }
            if(Vector3.Distance(worldPosition,startPos) < minDistance)
            {
                Debug.Log("Destroy");
                Destroy(currentTree);
                return;
            }

            //Debug.Log("End drag");
            if (curNode.placedObj != null)
            {
				Debug.Log("There's a tree here already");
                Destroy(currentTree);
                currentTree = null;
                currentTreeProperties = null;
            }
            else
            {
                currentTreeProperties.gridPosX = curNode.nodePosX;
                currentTreeProperties.gridPosZ = curNode.nodePosZ;
				currentTreeProperties.isPlaced = true;

                levelManager.inSceneGameObjects.Add(currentTree);

				if(currentTree.tag == "largeTree"){
					budgetManger.DecrementBudget(true);
					UpdateOccupiedPosition(largeTreeMultiplier);
				}
				else{ budgetManger.DecrementBudget(false);
					UpdateOccupiedPosition(medTreeMultiplier);
				}
                //budgetManger.DecrementBudget();
				//UpdateOccupiedPosition();

                
                levelManager.UpdateCanopyScore();

                curNode.placedObj = currentTreeProperties;
                currentTree = null;
				//Debug.Log("Place Tree");
            }

            cameraController.MovementEnabled = true;
            currentTree = null;
        }


        private void UpdateOccupiedPosition(int multiplier)
        {
            int x = curNode.nodePosX;
            int z = curNode.nodePosZ;

			gridBase.grid[x, z].shaded = true;
			gridBase.grid[x, z].multiplier = multiplier; 
			/*
            if (currentTree.name.Equals("LargerTree"))
            {
                Node[,] grid = gridBase.grid;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = true;
                grid[x, Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = true;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = true;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), z].shaded = true;
                grid[x, z].shaded = true;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), z].shaded = true;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = true;
                grid[x, Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = true;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = true;
            }
            else
            {
                gridBase.grid[x, z].shaded = true;
            }*/
        }

        public void ClearOccupiedPosition()
        {
            int x = curNode.nodePosX;
            int z = curNode.nodePosZ;

			gridBase.grid[x, z].shaded = false;
			/*
			if (currentTree.name.Equals("LargerTree"))
            {
                Node[,] grid = gridBase.grid;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = false;
                grid[x, Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = false;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z + 1, 0, gridBase.sizeZ - 1)].shaded = false;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), z].shaded = false;
                grid[x, z].shaded = false;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), z].shaded = false;
                grid[Mathf.Clamp(x - 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = false;
                grid[x, Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = false;
                grid[Mathf.Clamp(x + 1, 0, gridBase.sizeX - 1), Mathf.Clamp(z - 1, 0, gridBase.sizeZ - 1)].shaded = false;
            }
            else
            {
                gridBase.grid[x, z].shaded = false;
            }*/
        }


        void DeleteObjects()
        {
            if (deleteModeOn)
            {
                UpdateMousePosition();
                curNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !ui.mouseOverUIElement)
                {
                    if (curNode.placedObj != null)
                    {
                        if (levelManager.inSceneGameObjects.Contains(curNode.placedObj.gameObject))
                        {
							if(curNode.placedObj.gameObject.tag == "largeTree"){
								budgetManger.IncrementBudget(true);
							}
							else{budgetManger.IncrementBudget(false);}

                            levelManager.inSceneGameObjects.Remove(curNode.placedObj.gameObject);                            
                            Destroy(curNode.placedObj.gameObject);
							curNode.shaded = false;
							levelManager.UpdateCanopyScore();


                        }
						//currentTree = curNode.placedObj.gameObject; 
						//ClearOccupiedPosition();
                        curNode.placedObj = null;


                    }
                }
            }
        }

        #endregion

        #region Tile Painting
        bool paintOn = false;
        public void TogglePaintTile()
        {
            if (!paintOn)
            {
                paintOn = true;
                matToPlace = ResourceManager.GetInstance().GetMaterial(1);
                multiplier = MultiplierFromMatId(1);
            }
            else
            {
                paintOn = false;
            }
        }
        void PaintTile()
        {
            if (paintOn)
            {
                UpdateMousePosition();

                Node newNode = gridBase.NodeFromWorldPosition(mousePosition);

                if (Input.GetMouseButtonDown(0) && !uiSpace.IsPointerOverGameObject())
                {
                    newNode.tileRenderer.material = matToPlace;
                    newNode.vis.transform.localRotation = targetRot;
                    int matId = ResourceManager.GetInstance().GetMaterialId(matToPlace);
                    NodeObject nodeObj = newNode.vis.GetComponent<NodeObject>();
                    nodeObj.textureId = matId;
                    nodeObj.multiplier = multiplier;
                }
                else if (Input.GetMouseButtonDown(1) && !uiSpace.IsPointerOverGameObject())
                {
                    origMaterial = ResourceManager.GetInstance().GetMaterial(0);
                    newNode.tileRenderer.material = origMaterial;
                    newNode.vis.transform.localRotation = targetRot;
                    NodeObject nodeObj = newNode.vis.GetComponent<NodeObject>();
                    nodeObj.textureId = 0;
                    nodeObj.multiplier = 1;
                }
            }
        }

        public void PassMaterialToPaint(int matId)
        {
            matToPlace = ResourceManager.GetInstance().GetMaterial(matId);
            multiplier = MultiplierFromMatId(matId);
        }

        private int MultiplierFromMatId(int matId)
        {
            int multi;
            switch (matId)
            {
                case 0:
                    multi = 1;
                    break;
                case 1:
                    multi = 0;
                    break;
                default:
                    multi = 1;
                    break;
            }
            return multi;
        }
        #endregion

    }
}

