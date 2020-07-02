using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UrbanForestryQuest
{
    public abstract class InfoBox : MonoBehaviour
    {

        [SerializeField] protected TextMeshProUGUI displayedText;
        protected Queue<string> sentences;
        protected Queue<GameObject> sentenceImages;
        GameObject previousImage;

        float typeSpeed;

        bool displayingSentence;
       // [SerializeField] bool debugMode;


        private void Awake()
        {
            typeSpeed = GlobalControl.Instance.typeSpeed;
            sentences = new Queue<string>();
            sentenceImages = new Queue<GameObject>();
        }

        protected void LoadText()
        {
            sentences.Clear();

            foreach (string sentence in sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (!displayingSentence || GlobalControl.Instance.debugModeOn)
            {
                if (sentences.Count == 0)
                {
                    Debug.Log("No sentences");
                    HandleNoSentencesLeft();
                    return;
                }

                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }

        IEnumerator TypeSentence(string sentence)
        {
            displayingSentence = true;
            displayedText.text = "";
            if (sentenceImages.Count > 0)
            {
                DisplayNextImage();
            }
            
            foreach (char letter in sentence)
            {
                displayedText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
            
            displayingSentence = false;
        }
        
        void DisplayNextImage()
        {
            if (sentenceImages == null)
            {
                Debug.Log("No Images in Queue");
                return;
            }
            // get the next image in queue and turn it on
            GameObject sentenceImage = sentenceImages.Dequeue();
            sentenceImage.SetActive(true);

            //turn off previous image if there is one
            if (previousImage != null)
            {
                previousImage.SetActive(false);
            }
            previousImage = sentenceImage; // set previous image
                                           //Debug.Log("DISPLAYING IMAGE");
        }

        public abstract void HandleNoSentencesLeft();
        public abstract void LoadImages();
    }


}

