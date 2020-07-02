using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class BlockSceneEndTextbox : SolarInfoBox
    {
        [SerializeField] GameObject tapToContinueText;
        [SerializeField] GameObject levelLoader;
        [SerializeField] GameObject endButtons;

        [SerializeField] GameObject highScore;
        public int endingResult;

        private void Start()
        {
            tapToContinueText.SetActive(false);
        }
        public override void HandleNoSentencesLeft()
        {
            levelLoader.GetComponent<LevelLoader>().LoadLevel(1);
        }

        public void SetEnding(int num)
        {
            endingResult = num;
            LoadText();
        }

        public override void LoadText()
        {
            switch(endingResult){
                case 0:
                break;
                case 1:
                sentences.Enqueue("You were able to produce at least 0.4 KiloWattHours (KWH). That's enough to light up some lights.");
                break;
                case 2:
                sentences.Enqueue("You were able to produce at least 1.8 KiloWattHours (KWH). That's enough to keep your fridge and TV working.");
                break;
                case 3:
                sentences.Enqueue("You were able to produce at least 14.2 KiloWattHours (KWH). That's enough to replace gas heating and warm up your home!");
                break;
                case 4:
                sentences.Enqueue("You were able to produce at least 48.7 KiloWattHours (KWH). That's enough to charge up an electric vehicle!");
                break;
                case 5:
                sentences.Enqueue("You were able to produce at least 66 KiloWattHours (KWH). That's enough to charge up an electric vehicle, warm up your home, and keep your fridge, TV and lights on AND have extra energy!");
                break;
                default:
                sentences.Enqueue("");
                break;

            }    

            if(!GlobalControl.Instance.solarQuestPlayed)
            {
                sentences.Enqueue("Thank you "+ GlobalControl.Instance.playerName  + " for helping me retrofit 3 houses on my street. Wasn’t that fun! ");
                sentences.Enqueue("Together we were able to achieve " + GlobalControl.Instance.solarQuestHighScore  + "% of the solar potential for the 3 houses you selected.");
                sentences.Enqueue("We can try again if you want to help me do better, or, if you haven't already met Cam let's go talk with him!");
                GlobalControl.Instance.solarQuestPlayed = true;
            }
            else
            {
                sentences.Enqueue("Thank you "+ GlobalControl.Instance.playerName  + " for helping me retrofit 3 houses on my street. Wasn’t that fun! ");
            }
            tapToContinueText.SetActive(true);
            endButtons.SetActive(false);

            DisplayNextSentence();
            highScore.SetActive(false);
        }

		public override void LoadImages()
		{
			
		}
    }

}
