using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class SolarQuestIntroductionBox : SolarInfoBox
    {
        [SerializeField] GameObject solarQuestTutorialManager;

		public GameObject[] images; 

		void Start(){ // make sure all images are not visible
			foreach(GameObject g in images){
				g.SetActive(false);
			}
		}

        public override void LoadText()
        {			
			sentences.Enqueue("Hey! Thanks for tagging along " + GlobalControl.Instance.playerName + ". My name's Renee!");
			sentences.Enqueue("They call me 'Renewable' Renee because I believe the best way to combat climate change is by getting off fossil fuels and switching to renewable energy sources like wind, solar, geothermal, or hydro");
			sentences.Enqueue("Greenhouse gases are produced when we burn fossil fuels for heating, electricity, and transportation."); 
			sentences.Enqueue("We need to reduce our dependence on fossil fuels and use renewable energy sources.");
            sentences.Enqueue("Vancouver has a Renewable City Strategy. It's goal is to reduce greenhouse gas emission by 80% and switch to 100% renewable energy sources by 2050!");
            //sentences.Enqueue("Isn't that amazing! So here’s how we can help achieve this goal.");
            sentences.Enqueue("Let's help Our Future Community by using more solar panels! But before that, let’s get you familiarized with some key concepts with this tutorial.");

            LoadImages();
			DisplayNextSentence();
        }

        public override void HandleNoSentencesLeft()
        {
            solarQuestTutorialManager.GetComponent<SolarQuestTutorialManager>().ChangeStateToSliderTutorial();
        }

		public override void LoadImages()
		{
			//Debug.Log("Loading Images");
			foreach(GameObject g in images)
			{
				sentenceImages.Enqueue(g);
			}
		}
    }
}

