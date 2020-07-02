using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanForestryQuest
{
    public class ButtonToggle : MonoBehaviour
    {
        [SerializeField] Sprite normalImg;
        [SerializeField] Sprite pressedImg;
        private Button button;
        private bool toggleOn;
        // Start is called before the first frame update
        void Start()
        {
            toggleOn = false;
            button = GetComponent<Button>();
        }

        public bool On
        {
            get { return toggleOn; }
            set { toggleOn = value; }
        }

        private void Update()
        {
            ToggleButton();
        }

        private void ToggleButton()
        {
            if(toggleOn)
            {
                button.image.sprite = pressedImg;
                //ColorBlock cb = button.colors;
                //cb.normalColor = Color.green;
                //cb.highlightedColor = Color.green;
                //cb.selectedColor = Color.green;
                //button.colors = cb;
            }
            else
            {
                button.image.sprite = normalImg;
                //ColorBlock cb = button.colors;
                //cb.normalColor = Color.white;
                //cb.highlightedColor = Color.white;
                //cb.selectedColor = Color.white;
                //button.colors = cb;
            }
        }
    }
}

