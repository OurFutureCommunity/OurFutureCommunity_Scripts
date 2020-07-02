using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace SolarQuest
{
    public abstract class SolarInfoBox : MonoBehaviour
    {

        [SerializeField] protected TextMeshProUGUI displayedText;
        protected Queue<string> sentences;
		protected Queue<GameObject> sentenceImages;
		GameObject previousImage;

        float typeSpeed;

        bool displayingSentence = false;
        //[SerializeField] bool debugMode;


        private void Awake()
        {
            typeSpeed = GlobalControl.Instance.typeSpeed;
            sentences = new Queue<string>();
			sentenceImages = new Queue<GameObject>();
        }

        

        public void DisplayNextSentence()
        {
            if (!displayingSentence || GlobalControl.Instance.debugModeOn)
            {
                if (sentences.Count == 0)
                {
                    HandleNoSentencesLeft();
                    return;
                }

                string sentence = sentences.Dequeue();

                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }

		void DisplayNextImage(){
			if(sentenceImages.Count == 0) {
				Debug.Log("No Images in Queue");
				return;
			}
			// get the next image in queue and turn it on
			GameObject sentenceImage = sentenceImages.Dequeue();
			sentenceImage.SetActive(true);

			//turn off previous image if there is one
			if(previousImage != null){
				previousImage.SetActive(false);
			}
			previousImage = sentenceImage; // set previous image
			//Debug.Log("DISPLAYING IMAGE");
		}

        IEnumerator TypeSentence(string sentence)
        {
            displayingSentence = true;
            displayedText.text = "";
			DisplayNextImage();
            foreach (char letter in sentence)
            {
                displayedText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
            displayingSentence = false;
        }

        public abstract void LoadText();
		public abstract void LoadImages();
        public abstract void HandleNoSentencesLeft();
    }
}


