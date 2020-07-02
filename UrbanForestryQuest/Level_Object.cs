using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class Level_Object : MonoBehaviour
    {
        public string obj_Id;
        public int gridPosX;
        public int gridPosZ;
        public GameObject modelVisualization;
        public Vector3 worldPositionOffset;
        public Vector3 worldRotation;
        public bool inNoTreeZone;
		public bool isPlaced;

        public bool deleteTree;

        public float rotateDegrees = 90f;
        // Placing level object on grid
        public void UpdateNode(Node[,] grid)
        {
            // Find node from grid
            Node node = grid[gridPosX, gridPosZ];

            Vector3 worldPosition = node.vis.transform.position;
            worldPosition += worldPositionOffset;
            transform.rotation = Quaternion.Euler(worldRotation);
            transform.position = worldPosition;
        }

        public void ChangeRotation()
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles += new Vector3(0, rotateDegrees, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }

        // Function to create object that is to be saved
        public SaveableLevelObject GetSaveableObject()
        {
            SaveableLevelObject savedObj = new SaveableLevelObject();
            savedObj.obj_Id = obj_Id;
            savedObj.posX = gridPosX;
            savedObj.posZ = gridPosZ;

            worldRotation = transform.localEulerAngles;

            savedObj.rotX = worldRotation.x;
            savedObj.rotY = worldRotation.y;
            savedObj.rotZ = worldRotation.z;

            return savedObj;
        }

    }

    [System.Serializable]
    public class SaveableLevelObject
    {
        public string obj_Id;
        public int posX;
        public int posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
    }
}


