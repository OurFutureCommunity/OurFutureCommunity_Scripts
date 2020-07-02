using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

namespace KitsilanoScene
{
    public class IntroductionSequence : MonoBehaviour
    {
        //public bool debugMode;
        float typeSpeed;
        public float fadeOutTime;
        public float fadeInTime;
        public GameObject characterDialogue;
        public GameObject textboxRenee;
        public TextMeshProUGUI displayedTextRenee;
        public GameObject textboxCam;
        public TextMeshProUGUI displayedTextCam;
        private TextMeshProUGUI displayedText;
        public Queue<Dialogue> conversation;
        private bool displayingSentence;

        public PlayableDirector cutscene;
        private bool cutscenePlayed;

        bool endScenePlayed;

        void Start()
        {
            typeSpeed = GlobalControl.Instance.typeSpeed;
        }
        private void OnEnable()
        {
            /*
            conversation = new Queue<Dialogue>();
			conversation.Enqueue(new Dialogue("r", "Hi " + GlobalControl.Instance.playerName + "!"));
			conversation.Enqueue(new Dialogue("r", "We are grade 12 students who are part of the ‘Climate Ambassador’ Club at Kitsilano Secondary. We’ve heard you’re concerned about climate change and want to help, but don’t know where to start."));
			conversation.Enqueue(new Dialogue("c", "It's okay! You’ve come to the right place. Climate change is a pretty big problem and we need everyone’s help to help find a solution that best for our community."));
			conversation.Enqueue(new Dialogue("r", "That's right! I am happy to share some of the mitigation strategies I know like using renewable energy & how that can help reduce our carbon emissions."));
			conversation.Enqueue(new Dialogue("r", "Mitigating climate change means decreasing our contribution to climate change. Finding solutions to fix/stop the source of all emissions and greenhouse gases (GHGs)."));
			conversation.Enqueue(new Dialogue("c", "On the other hand, I know a lot about adaptation strategies, like planting trees in our communities."));
			conversation.Enqueue(new Dialogue("c", "Adapting to climate change the process of adjusting to the actual or expected climate and its effects."));
            conversation.Enqueue(new Dialogue("r", "Your timing is perfect. Our Climate Ambassador Club just got approved to test 2 small student-led pilot projects in Kitsilano on the blocks we live in."));
            conversation.Enqueue(new Dialogue("c", "We can't wait for you to join us, " + GlobalControl.Instance.playerName + "!"));
			*/
        }

        public void StartIntroductionSequence()
        {
            conversation = new Queue<Dialogue>();
			conversation.Enqueue(new Dialogue("r", "Hi " + GlobalControl.Instance.playerName + "!"));
			conversation.Enqueue(new Dialogue("r", "We are grade 12 students who are part of the ‘Climate Ambassador’ Club at Kitsilano Secondary. We’ve heard you’re concerned about climate change and want to help, but don’t know where to start."));
			conversation.Enqueue(new Dialogue("c", "It's okay! You’ve come to the right place. We need everyone’s help to find a solution that's best for our community."));
			conversation.Enqueue(new Dialogue("r", "That's right! I am happy to share some of the mitigation strategies I know like using renewable energy & how that can help reduce our carbon emissions."));
			conversation.Enqueue(new Dialogue("r", "Mitigating climate change means decreasing our contribution to climate change. Finding solutions to fix/stop the source of all emissions and greenhouse gases (GHGs)."));
			conversation.Enqueue(new Dialogue("c", "On the other hand, I know a lot about adaptation strategies, like planting trees in our communities."));
			conversation.Enqueue(new Dialogue("c", "Adapting to climate change is the process of adjusting to the actual or expected climate and its effects."));
            conversation.Enqueue(new Dialogue("r", "Your timing is perfect. Our Climate Ambassador Club just got approved to test 2 small student-led pilot projects in Kitsilano on the blocks we live in."));
            conversation.Enqueue(new Dialogue("c", "We can't wait for you to join us, " + GlobalControl.Instance.playerName + "!"));

            StartCoroutine(FadeIn(characterDialogue));
        }

        public void DisplayNextSentence()
        {
            if (!displayingSentence || GlobalControl.Instance.debugModeOn)
            {
                if (conversation.Count == 0)
                {
                    if(!cutscenePlayed)
                    {
                        // intro cutscene
                        PlayCutscene();
                    }
					else
					{
                        // end cutscene
                        endScenePlayed = true;
						StartCoroutine(FadeAway(characterDialogue));                        
					}
                    return;
                }

                Dialogue dialogue = conversation.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(dialogue.character, dialogue.text));
            }
        }

        private void PlayCutscene()
        {
            cutscenePlayed = true;
            GlobalControl.Instance.introCutscenePlayed = true;            
            cutscene.Play(); 
            StartCoroutine(FadeAway(characterDialogue));
            
        }

        IEnumerator TypeSentence(string character, string sentence)
        {
            displayingSentence = true;

            if (character.Equals("r")) //Renee
            {
                textboxRenee.SetActive(true);
                textboxCam.SetActive(false);
                displayedText = displayedTextRenee;
            }
			else if (character.Equals("c")) //Cam
            {
                textboxRenee.SetActive(false);
                textboxCam.SetActive(true);
                displayedText = displayedTextCam;
            }
            displayedText.text = "";
            foreach (char letter in sentence)
            {
                displayedText.text += letter;
                yield return new WaitForSeconds(typeSpeed);
            }
            displayingSentence = false;
        }

        private IEnumerator FadeAway(GameObject objectToFade)
        {           
            CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();

            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, Mathf.Min(1, t / fadeOutTime));
                yield return null;
            }
            canvasGroup.alpha = 0;
            objectToFade.SetActive(false);

            // for end cutscene
            if(endScenePlayed)
            {
                KitsilanoManager.GetInstance().CurrentState = KitsilanoManager.GameState.Credits;
                //KitsilanoManager.GetInstance().CurrentState = KitsilanoManager.GameState.MainMenu;
            }
        }

        private IEnumerator FadeIn(GameObject objectToFade)
        {           
            CanvasGroup canvasGroup = objectToFade.GetComponent<CanvasGroup>();
            characterDialogue.SetActive(true);
            canvasGroup.alpha = 0;
            
            for (float t = 0.01f; t < fadeInTime; t += Time.deltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, Mathf.Max(0, t / fadeInTime));
                yield return null;
            }
            canvasGroup.alpha = 1;
            
            //objectToFade.SetActive(false);
            
            DisplayNextSentence();
            
        }

		public void StartEndSequence()
		{
            cutscenePlayed = true;

			conversation = new Queue<Dialogue>();
			conversation.Enqueue(new Dialogue("r", "Hi " + GlobalControl.Instance.playerName + "! Thanks for all your work! I hope you learned more about solar panels."));
			conversation.Enqueue(new Dialogue("c", "Yeah! Amazing work " + GlobalControl.Instance.playerName + "! I hope you learned more about canopy coverage."));
			conversation.Enqueue(new Dialogue("r", "If you want to improve your scores you can always try again!"));
			conversation.Enqueue(new Dialogue("c", "Hope to see you around!"));

			StartCoroutine(FadeIn(characterDialogue));
		}
    }
}

