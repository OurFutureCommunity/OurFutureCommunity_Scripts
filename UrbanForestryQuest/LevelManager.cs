using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UrbanForestryQuest
{
    public class LevelManager : MonoBehaviour
    {
    public bool UFHouseScene;

        GridBase gridBase;

        public List<GameObject> inSceneGameObjects = new List<GameObject>();
        List<GameObject> deadTrees = new List<GameObject>();
        private List<Level_Object> internalSceneObjects = new List<Level_Object>();

        public GameObject canopyBar;
        public GameObject endCanopyBar;

        // SCORING
        private float canopyScore;
        [SerializeField] float startScore;
        [SerializeField] float maxScore;

        [SerializeField] float totalArea = 720;
        [SerializeField] float existingOccupiedSquares = 264;
        private float availableArea;
        [SerializeField] float areaPerTree = 16;
        [SerializeField] float mortalityRate = 0.05f;

        // Used as a parent to instantiate increments in 
        [SerializeField] GameObject canopyBarBorder;
        [SerializeField] float canopyBarFillHeight;
        [SerializeField] GameObject incrementCanopyBar;

        [SerializeField] GameObject nonpermBorders;
        private float scoreDiff;
        private float newScore;
        private float treeGrowth = 1.3f;



        // Game Stats
        [SerializeField] TextMeshProUGUI totalText;
        [SerializeField] TextMeshProUGUI surviveCountText;
        [SerializeField] TextMeshProUGUI nonpermCountText;
        [SerializeField] TextMeshProUGUI chanceCountText;

        public int canopyCoverPercentage = 0;

        public float CanopyScore
        {
            get
            {
                canopyScore = newScore;
                return ((canopyScore * areaPerTree) / 720) * 100;
            }
        }

        // FUTURE VISUALIZATION
        #region Future Visualization
        [SerializeField] GameObject tree_large;
        [SerializeField] GameObject tree_dead;
        [SerializeField] GameObject deadNonpermIndicator;
        [SerializeField] GameObject deadChanceIndicator;
        private List<GameObject> futureTrees = new List<GameObject>();
        [SerializeField] GameObject existingTrees;
        [SerializeField] GameObject agedExistingTrees;
		int nonpermTreeCount = 0;
        int chanceTreeCount = 0;
        int surviveTreeCount = 0;
        #endregion

        #region Singleton
        private static LevelManager instance = null;
        public static LevelManager GetInstance()
        {
            return instance;
        }

        private void Awake()
        {
            instance = this;
        }
        #endregion

        private void Start()
        {
            gridBase = GridBase.GetInstance();
            nonpermBorders.SetActive(true);
            // Canopy bar starts at 0
            canopyScore = startScore;
			canopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);
			//canopyBar.transform.localScale = new Vector3(1, startScore / maxScore, 1);
            //increment = Instantiate(incrementPrefab, canopyBar.transform.position, Quaternion.identity) as GameObject;
            //increment.transform.SetParent(canopyBarBorder.transform);
            //increment.transform.localScale = new Vector3(1, 0, 1);
            InitLevelObjects();

            totalArea = gridBase.sizeX * gridBase.sizeZ;
            availableArea = totalArea - existingOccupiedSquares;
        }

        // Used to load objects if there is save function
        void InitLevelObjects()
        {
            if (inSceneGameObjects.Count > 0)
            {
                for (int i = 0; i < inSceneGameObjects.Count; i++)
                {
                    Level_Object obj = inSceneGameObjects[i].GetComponent<Level_Object>();
                    obj.UpdateNode(gridBase.grid);
                }
            }
        }

        public void ResetLevelObjects()
        {
            if (inSceneGameObjects.Count > 0)
            {
                Node[,] grid = gridBase.grid;

                //make all the grid placements not shades
                for (int x = 0; x < gridBase.sizeX; x++)
                {
                    for (int z = 0; z < gridBase.sizeZ; z++)
                    {
                        if (grid[x, z].shaded)
                        {
                            grid[x, z].shaded = false;
                        }

                    }
                }

                // delete all the trees in scene
                for (int i = 0; i < inSceneGameObjects.Count; i++)
                {
                    Destroy(inSceneGameObjects[i]);
                }
                inSceneGameObjects = new List<GameObject>();

                //delete all deadTrees
                for (int i = 0; i < deadTrees.Count; i++)
                {
                    Destroy(deadTrees[i]);
                }
                deadTrees = new List<GameObject>();

                //reset original trees
                existingTrees.SetActive(true);
                agedExistingTrees.SetActive(false);

                // reset score
                canopyScore = startScore;
                newScore = 0;
                incrementCanopyBar.transform.localScale = new Vector3(1, 0, 1);
                canopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);
                UpdateCanopyScore();

                // reset tree counts
                nonpermTreeCount = 0;
                chanceTreeCount = 0;
                surviveTreeCount = 0;

                // reset budget
                BudgetManager.GetInstance().ResetBudget();
            }
        }

        //public void UpdateCanopyScore()
        //{
        //    internalSceneObjects.Clear();
        //    newScore = startScore;
        //    for (int i = 0; i < inSceneGameObjects.Count; i++)
        //    {
        //        Level_Object lvlObj = inSceneGameObjects[i].GetComponent<Level_Object>();
        //        internalSceneObjects.Add(lvlObj);
        //        newScore += gridBase.grid[lvlObj.gridPosX, lvlObj.gridPosZ].multiplier;
        //    }
        //    newScore = Mathf.Clamp(newScore, 0f, maxScore);
        //    scoreDiff = newScore - canopyScore;

        //    increment.transform.position = canopyBar.transform.position + new Vector3(0, canopyBarFillHeight * (canopyScore / maxScore), 0);
        //    if (scoreDiff >= 0)
        //    {
        //        increment.transform.localScale = new Vector3(1, scoreDiff / maxScore, 1);
        //    }
        //    else
        //    {
        //        increment.transform.localScale = new Vector3(1, 0, 1);
        //    }

        //    canopyScore = newScore;
        //    canopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);
        //}

		/*
        public void UpdateCanopyScore()
        {
            Node[,] grid = gridBase.grid;

            internalSceneObjects.Clear();
            //newScore = startScore;
			canopyScore = startScore;
			for (int x = 0; x < gridBase.sizeX; x++)
            {
                for (int z = 0; z < gridBase.sizeZ; z++)
                {
                    if (grid[x, z].shaded)
                    {
						//Debug.Log("Multiplier");
						//Debug.Log(grid[x, z].multiplier);
                        newScore += grid[x, z].multiplier;
                    }

                }
            }

            newScore = Mathf.Clamp(newScore, 0f, totalArea);
            scoreDiff = newScore - canopyScore;

            increment.transform.position = canopyBar.transform.position + new Vector3(0, canopyBarFillHeight * (canopyScore / availableArea), 0);
            if (scoreDiff >= 0)
            {
                increment.transform.localScale = new Vector3(1, scoreDiff / availableArea, 1);
            }
            else
            {
                increment.transform.localScale = new Vector3(1, 0, 1);
            }

            canopyScore = newScore;
			//canopyBar.transform.localScale = new Vector3(1, canopyScore / availableArea, 1);
			Debug.Log(canopyScore);
			Debug.Log(canopyScore / availableArea);
			canopyBar.transform.localScale = new Vector3(1, canopyBar.transform.localScale.y + (canopyScore / availableArea), 1);
        }*/

		public void UpdateCanopyScore()
		{
			Node[,] grid = gridBase.grid;

			internalSceneObjects.Clear();
			newScore = startScore;
			//Debug.Log("New Score");
			//Debug.Log(newScore);
			//canopyScore = startScore;
			for (int x = 0; x < gridBase.sizeX; x++)
			{
				for (int z = 0; z < gridBase.sizeZ; z++)
				{
					if (grid[x, z].shaded)
					{
						//Debug.Log("Multiplier");
						//Debug.Log(grid[x, z].multiplier);
						newScore += grid[x, z].multiplier;
					}

				}
			}

			newScore = Mathf.Clamp(newScore, 0f, totalArea);

            if(newScore <= canopyScore)
            {
                canopyScore = newScore;
                incrementCanopyBar.transform.localScale = new Vector3(1, newScore/ maxScore, 1);
                canopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);                
            }
            else{
                incrementCanopyBar.transform.localScale = new Vector3(1, newScore/ maxScore, 1);
                canopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);
                canopyScore = newScore;
            }
        }

        void ChangeTreeIndicatorSize(GameObject indicator, string tag)
        {
            if(tag == "medTree")
            {
                indicator.transform.localScale = new Vector3(3,5,2);
            }
            else
            {
                indicator.transform.localScale = new Vector3(7,2,4);
            }
        }

		public void KillNonperableGrowLiveTrees(){
            Node[,] grid = gridBase.grid;
            // if tree is on concrete or touching another tree, kill the tree
            // else grow the tree by treeGrowth
            foreach (GameObject tree in inSceneGameObjects){
				if (tree.GetComponent<Level_Object>().inNoTreeZone)
				{
					GameObject deadTree = Instantiate(tree_dead, tree.transform.position, Quaternion.identity);
					GameObject deadTreeIndicator = Instantiate(deadNonpermIndicator, tree.transform.position, Quaternion.identity);
                    ChangeTreeIndicatorSize(deadTreeIndicator,tree.tag);
                    deadTrees.Add(deadTree);
                    deadTrees.Add(deadTreeIndicator);
					tree.SetActive(false);
                    // turn off that grid point
                    grid[tree.GetComponent<Level_Object>().gridPosX, tree.GetComponent<Level_Object>().gridPosZ].shaded = false;
                    nonpermTreeCount++;
				}
				else
				{
					tree.transform.localScale = tree.transform.localScale * treeGrowth;
				}
			}
		}

        // Last step before the quest finishes
        public void VisualizeFuture()
        {
            nonpermBorders.SetActive(false);
            Node[,] grid = gridBase.grid;
            int numberOfInSceneGameObjects = inSceneGameObjects.Count;
			//Debug.Log(numberOfInSceneGameObjects);
            if (numberOfInSceneGameObjects > 0)
            {
                /*
                for (int i = 0; i < numberOfInSceneGameObjects; i++)
                {
                    Level_Object lvlObj = internalSceneObjects[i];

                    // Only if a tree is placed in the right spot does it get added to list of future trees 
                    if (gridBase.grid[lvlObj.gridPosX, lvlObj.gridPosZ].multiplier != 0)
                    {
                        GameObject newTree = Instantiate(tree_large, inSceneGameObjects[i].transform.position, Quaternion.identity);
                        inSceneGameObjects[i].SetActive(false);
                        futureTrees.Add(newTree);
                    }
                    else
                    {
                        inSceneGameObjects[i].SetActive(false);
                    }
                }
               

                int deadTreeCount = 0;
				foreach(GameObject tree in inSceneGameObjects){
                    if (tree.GetComponent<Level_Object>().inNoTreeZone)
                    {
                        Instantiate(tree_dead, tree.transform.position, Quaternion.identity);                        
                        tree.SetActive(false);
                        deadTreeCount++;
                    }
                    else
                    {
                        tree.transform.localScale = tree.transform.localScale * treeGrowth;
                    }
				}
				*/
				futureTrees = inSceneGameObjects;

                // Replace dead trees with dead tree visualization for chance trees
                int numberOfFutureTrees = futureTrees.Count - nonpermTreeCount;
                int numberOfDeadTrees = Mathf.RoundToInt(mortalityRate * numberOfFutureTrees);
                chanceTreeCount = numberOfDeadTrees;

                while (numberOfDeadTrees > 0)
                {
                    int randomNumber = Mathf.RoundToInt(Random.Range(0, numberOfFutureTrees - 1));
                    if (futureTrees[randomNumber].activeSelf)
                    {
                        grid[futureTrees[randomNumber].GetComponent<Level_Object>().gridPosX, futureTrees[randomNumber].GetComponent<Level_Object>().gridPosZ].shaded = false;
                        GameObject deadtree = Instantiate(tree_dead, futureTrees[randomNumber].transform.position, Quaternion.identity);
                        GameObject deadtreeIndicator = Instantiate(deadChanceIndicator, futureTrees[randomNumber].transform.position, Quaternion.identity);
                        ChangeTreeIndicatorSize(deadtreeIndicator,futureTrees[randomNumber].tag);
                        deadTrees.Add(deadtree);
                        deadTrees.Add(deadtreeIndicator);
                        futureTrees[randomNumber].SetActive(false);
                        numberOfDeadTrees--;
                    }
                }
                

                existingTrees.SetActive(false);
                agedExistingTrees.SetActive(true);
            }

            // real canopy score            
            canopyScore = startScore;
            for (int x = 0; x < gridBase.sizeX; x++)
            {
                for (int z = 0; z < gridBase.sizeZ; z++)
                {
                    if (grid[x, z].shaded)
                    {
                        surviveTreeCount++;
                        canopyScore += grid[x, z].multiplier;
                    }

                }
            }

            endCanopyBar.transform.localScale = new Vector3(1, canopyScore / maxScore, 1);
            canopyCoverPercentage = Mathf.RoundToInt(endCanopyBar.transform.localScale.y * 0.5f*100f);
            //Debug.Log(canopyCoverPercentage);
            // display stats
            int total = surviveTreeCount + nonpermTreeCount + chanceTreeCount;
            totalText.text = total.ToString();
            surviveCountText.text = surviveTreeCount.ToString();
            nonpermCountText.text = nonpermTreeCount.ToString();
            chanceCountText.text = chanceTreeCount.ToString();
            
            // Sets the high score for the quest
            int currentHS = GlobalControl.Instance.ufQuestHighScore;
            if(!UFHouseScene && currentHS < canopyCoverPercentage)
            {
                GlobalControl.Instance.ufQuestHighScore = canopyCoverPercentage;
				PlayerPrefs.SetInt("UFScore", canopyCoverPercentage);
                UFBlockManager.GetInstance().DisplayNewHighscore(canopyCoverPercentage);                
            }
        }


       
		/*
		private void KillTreesNextToEachOther()
		{
			Node[,] grid = gridBase.grid;

			for (int x = 0; x < gridBase.sizeX; x++)
			{
				for (int z = 0; z < gridBase.sizeZ; z++)
				{
					if (grid[x, z].shaded)
					{
						//Debug.Log("Multiplier");
						//Debug.Log(grid[x, z].multiplier);
						newScore += grid[x, z].multiplier;
					}

				}
			}
		}
		// return true if there are trees next to this tree
		bool NeighborsHaveTrees(string tag, int x, int z)
		{
			Node[,] grid = gridBase.grid;

			if(tag == "largeTree")
			{
				if (grid[x, z].shaded)
			}
		}*/


    }
}

