using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
	public class BlockIntroBox : InfoBox
	{
		private void Start()
		{
			//Debug.Log("Loading");
			sentences.Enqueue("Now that you got the hang of planting trees, let's plant some more on my street!");
            sentences.Enqueue("We have just received City of Vancouver's Neighbourhood Matching Fund of $2,000 for a greening project on this block.");
            sentences.Enqueue("Let's get started! Remember to keep in mind the canopy in 2050!");

			DisplayNextSentence();
			//Debug.Log("Display Next");
		}

		public override void HandleNoSentencesLeft()
		{
			UFBlockManager.GetInstance().CurrentState = UFBlockManager.GameState.Tutorial;
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
