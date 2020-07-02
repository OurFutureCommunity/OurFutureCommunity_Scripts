using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class Node
    {
        // Node position on grid
        public int nodePosX;
        public int nodePosZ;

        // Square used to visualize the grid
        public GameObject vis;

        // Object placed in node
        public Level_Object placedObj;

        // Renderer for grid tile texture
        public MeshRenderer tileRenderer;

        // Multiplier of grid square for scoring purposes
        public int multiplier;

        public bool shaded;
    }
}

