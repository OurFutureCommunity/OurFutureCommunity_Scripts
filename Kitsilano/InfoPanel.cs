using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KitsilanoScene
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] GameObject background;

        [SerializeField] GameObject solarQuest;
        [SerializeField] GameObject ufQuest;

        [SerializeField] GameObject loaderScript;
        LevelLoader levelLoader;

        [SerializeField] TextMeshProUGUI solarHighScoreText;
        [SerializeField] TextMeshProUGUI ufHighScoreText;

        int selectedQuest;

        public bool solarQuesTutorialPlayed;

        public bool urbanForestryPlayed;

        // HIGH SCORES
        public int solarQuestHighScore = 0;
        public int ufQuestHighScore = 0;

        // Start is called before the first frame update
        void Start()
        {
            if (loaderScript)
            {
                levelLoader = loaderScript.GetComponent<LevelLoader>();
            }

            background.SetActive(false);
            solarQuest.SetActive(false);
            ufQuest.SetActive(false);

        }

        private void LoadData()
        {
            urbanForestryPlayed = GlobalControl.Instance.urbanForestryPlayed;
            solarQuesTutorialPlayed = GlobalControl.Instance.solarQuestPlayed;
            solarQuestHighScore = GlobalControl.Instance.solarQuestHighScore;
            ufQuestHighScore = GlobalControl.Instance.ufQuestHighScore;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowInfoPanel(int questNumber)
        {
            LoadData();
            selectedQuest = questNumber;
            background.SetActive(true);
            switch (questNumber)
            {
                case 0:
                    solarQuest.SetActive(true);
                    solarHighScoreText.text = solarQuestHighScore.ToString() + "% Solar Potential";
                    break;
                case 1:
                    ufQuest.SetActive(true);
                    ufHighScoreText.text = ufQuestHighScore.ToString() + "% Canopy Cover";
                    break;
                default:
                    Debug.Log("Invalid quest number!");
                    break;
            }
        }

        public void HideInfoPanel()
        {
            background.SetActive(false);
            switch (selectedQuest)
            {
                case 0:
                    solarQuest.SetActive(false);
                    break;
                case 1:
                    ufQuest.SetActive(false);
                    break;
                default:
                    Debug.Log("Invalid quest number!");
                    break;
            }
        }

        public void StartQuest()
        {
            switch (selectedQuest)
            {
                case 0:
				if (!solarQuesTutorialPlayed || solarQuestHighScore == 0)
                    {
                        levelLoader.LoadLevel(2);
                    }
                    else
                    {
                        levelLoader.LoadLevel(3);
                    }
                    
                    break;
                case 1:
				if(!urbanForestryPlayed || ufQuestHighScore == 0){
                    levelLoader.LoadLevel(4);}
                    else{
                        levelLoader.LoadLevel(5);
                    }
                    break;
                default:
                    Debug.Log("Invalid quest number!");
                    break;
            }
        }
    }

}
