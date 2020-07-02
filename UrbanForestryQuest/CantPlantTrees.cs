using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class CantPlantTrees : MonoBehaviour
    {
        void OnTriggerEnter(Collider col)
        {
           
            if (col.tag == "largeTree" || col.tag == "medTree" )
            {
                if (col.gameObject.GetComponent<Level_Object>() != null) 
                {
                    if (!col.gameObject.GetComponent<Level_Object>().inNoTreeZone)
                    {
                        col.gameObject.GetComponent<Level_Object>().inNoTreeZone = true;
                        Debug.Log("Tree has entered!");
                    }
                }
            }
        }
        // if collider leaves the noTreeZone
        void OnTriggerExit(Collider col)
        {            
            if (col.tag == "largeTree" || col.tag == "medTree" )
            {
                if (col.gameObject.GetComponent<Level_Object>() != null)
                {
                    if (col.gameObject.GetComponent<Level_Object>().inNoTreeZone)
                    {
                        col.gameObject.GetComponent<Level_Object>().inNoTreeZone = false;
                        Debug.Log("Tree has exited!");
                    }
                }
               
            }
        }
    }
}
