using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest{
    public class HouseQuizEndText : InfoBox
    {
        [SerializeField] GameObject quiz;        
        void Start()
        {            
            sentences.Enqueue("Ok, now that we know where to plant, let’s do a quick tutorial to learn how to get maximum tree canopy.");
            DisplayNextSentence();
        }
        public override void HandleNoSentencesLeft()
        {
            UrbanForestryQuestManager.GetInstance().CurrentState = UrbanForestryQuestManager.GameState.Tutorial;
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
