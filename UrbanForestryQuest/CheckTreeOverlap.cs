using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class CheckTreeOverlap : MonoBehaviour
    {
        public Level_Object thisLevelObject;
        public GameObject deleteCross;
		/*
        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "largeTree" || col.tag == "medTree" || col.tag == "noTrees")
            {
                if (!thisLevelObject.inNoTreeZone)
                {
                    thisLevelObject.inNoTreeZone = true;
                    Debug.Log("Tree has entered");
                }
            }
        }*/

        void Awake()
        {
            deleteCross.SetActive(false);
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "deleteTree")
            {
                deleteCross.SetActive(true);
                thisLevelObject.deleteTree = true;
            }
        }
        void OnTriggerStay(Collider col)
        {
            if(col.tag == "deleteTree")
            {
                deleteCross.SetActive(true);
                thisLevelObject.deleteTree = true;
            }
            else if (col.tag == "largeTree" || col.tag == "medTree" || col.tag == "noTrees")
            {
                //if (!thisLevelObject.inNoTreeZone)
				if (thisLevelObject.isPlaced && !thisLevelObject.inNoTreeZone)
                {
                    thisLevelObject.inNoTreeZone = true;
                    Debug.Log("Tree has stayed");
                }
            }
        }

        // if collider leaves the noTreeZone
        void OnTriggerExit(Collider col)
        {
            if(col.tag == "deleteTree")
            {
                deleteCross.SetActive(false);
                thisLevelObject.deleteTree = false;
            }
            if (col.tag == "largeTree" || col.tag == "medTree" || col.tag == "noTrees")
            {
                //if (thisLevelObject.inNoTreeZone)
				if (thisLevelObject.isPlaced && thisLevelObject.inNoTreeZone)
				{
                    thisLevelObject.inNoTreeZone = false;
                    Debug.Log("Tree has exitted");
                }

            }
        }
    }
}

