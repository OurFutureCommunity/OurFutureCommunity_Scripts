using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class EndTextBox : SolarInfoBox
    {
        public GameObject tapToContinueText;
        [SerializeField] GameObject levelLoader;
		public GameObject imageHolder;
		public GameObject[] images; 

		void Start(){ // make sure all images are not visible
			tapToContinueText.SetActive(false);
			imageHolder.SetActive(false);
			foreach(GameObject g in images){
				g.SetActive(false);
			}
		}

        public override void LoadText()
        {
			sentences.Enqueue("We’re lucky we live in BC, because you can choose to connect your solar panels to the electricity grid.");
            
            sentences.Enqueue("So when your solar panels produce surplus electricity, it gets fed to the grid for others to use.");

            sentences.Enqueue("This earns you BC Grid Credits! Which reduces the price you pay for electricity on days where there isn't much sunlight.");

            sentences.Enqueue("You're getting the hang of it! Let's now try to install solar panels for the whole street.");
            LoadImages();
            DisplayNextSentence();
        }


        public override void HandleNoSentencesLeft()
        {
            tapToContinueText.SetActive(false);
            //GlobalControl.Instance.solarQuestTutorialPlayed = true;
            levelLoader.GetComponent<LevelLoader>().LoadLevel(3);
        }

		public override void LoadImages()
		{
			foreach(GameObject g in images)
			{
				sentenceImages.Enqueue(g);
			}
		}
    }
}

