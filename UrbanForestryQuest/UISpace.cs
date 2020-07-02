using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UrbanForestryQuest
{
    public class UISpace : MonoBehaviour
    {
        private static UISpace instance = null;

        private void Awake()
        {
            instance = this;
        }

        public static UISpace GetInstance()
        {
            return instance;
        }

        public bool IsPointerOverGameObject()
        {
            if (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

