using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
	public class BlockEndBox : InfoBox
	{
		[SerializeField] GameObject ContinueButtons;

		private void Start()
		{
			ContinueButtons.SetActive(false);
			if(!GlobalControl.Instance.urbanForestryPlayed)	
			{
				sentences.Enqueue("It's now 2050. Thank you " + GlobalControl.Instance.playerName + " for planting those trees!");
				sentences.Enqueue("In the last 30 years, the global temperatures has drastically risen.");
				sentences.Enqueue("Because of which, more people, especially elders like me, are more vulnerable to heat stroke.");
				sentences.Enqueue("But the trees you planted provide me and my fellow residents plenty of shade, provide refuge and create a soothing cooling effect for our streets, cars, and homes.");
				GlobalControl.Instance.urbanForestryPlayed = true;
			}
			else
			{
				sentences.Enqueue("It's now 2050. Thank you again " + GlobalControl.Instance.playerName + " for planting those trees!");
			}
			DisplayNextSentence();
            
		}

		public override void HandleNoSentencesLeft()
		{
			ContinueButtons.SetActive(true);
			//UrbanForestryQuestManager.GetInstance().CurrentState = UrbanForestryQuestManager.GameState.Tutorial;
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

		void LoadEndText()
		{
			
		}
    }

}