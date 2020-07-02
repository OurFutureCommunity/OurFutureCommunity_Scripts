using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class EndBox : InfoBox
    {
        [SerializeField] GameObject buttons;
        [SerializeField] GameObject continueText;
        
        private void Start()
        {
            LoadText();
        }
        public override void HandleNoSentencesLeft()
        {
            continueText.SetActive(false);
            //displayedText.text = "The canopy cover is "+ canopyScorePercent +  "%. Would you like to try again?";

            buttons.SetActive(true);
        }

        public override void LoadImages()
        {
            /*
            //Debug.Log("Loading Images");
            foreach (GameObject g in images)
            {
                sentenceImages.Enqueue(g);
            }
            */
        }
    }
}

