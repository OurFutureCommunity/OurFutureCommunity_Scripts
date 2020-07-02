using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class SolarPanel : MonoBehaviour
    {
        // Whether panel has been placed on the grid or is still in pile 
        bool isPanelPlaced;


        public int gridRow = -1;
        public int gridCol = -1;

        private GridGenerator grid;
        // Start is called before the first frame update
        void Start()
        {
            isPanelPlaced = false;
        }

        public bool PanelPlaced
        {
            get { return isPanelPlaced; }
            set { isPanelPlaced = value; }
        }

        public GridGenerator Grid
        {
            get { return grid; }
            set { grid = value; }
        }


    }
}

