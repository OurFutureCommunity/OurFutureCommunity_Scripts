using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class IntroBox : InfoBox
    {
        public GameObject[] images;

        private void Start()
        {
            foreach (GameObject g in images)
            {
                g.SetActive(false);
            }

            sentences.Enqueue("Hi " + GlobalControl.Instance.playerName + "! I didn't introduce myself earlier, I'm Cam.");
            sentences.Enqueue("They call me 'Canopy Cam' because I believe planting trees in our communities is one of the easiest and most effective way to adapt to climate change!");
            sentences.Enqueue("Imagine it's the year 2050 and the temperature outside is 35 Celsius with no places for refuge with shade.");
            sentences.Enqueue("Trees help us not only with shade, but also protect communities against storms, floods, food shortages and extreme heat.");
            sentences.Enqueue("Do you know Vancouver has set a goal of increasing its tree canopy to 22% by 2050 by planting 150,000 new trees, under its Urban Forest Strategy.");
            sentences.Enqueue("However, research shows 22% may not be enough. Trees have a 6% mortality rate every year due to diseases, insects, improper watering/nutrition, etc.");
            sentences.Enqueue("Let's help Our Future Community by planting trees! Do you know where the best place to plant trees is?");
            //sentences.Enqueue("“The best time to plant a tree was yesterday!” So lets get to it! First, where is the best place to plant trees?");
            //sentences.Enqueue("First, lets find out WHERE to plant trees in order to give them the best chance of survival.");
            
            LoadImages();
            DisplayNextSentence();

        }
        /*
        private void Awake()
        {
            //LoadImages();
            
        }*/

        public override void HandleNoSentencesLeft()
        {
            UrbanForestryQuestManager.GetInstance().CurrentState = UrbanForestryQuestManager.GameState.Quiz;
        }
        public override void LoadImages()
        {
            
            //Debug.Log("Loading Images");
            foreach (GameObject g in images)
            {
                sentenceImages.Enqueue(g);
            }
            //Debug.Log("Done Loading");
        }
    }

}


