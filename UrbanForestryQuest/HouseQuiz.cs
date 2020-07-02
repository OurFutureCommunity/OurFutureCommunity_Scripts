using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UrbanForestryQuest
{
    public class HouseQuiz : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI quizText;
        [SerializeField] GameObject quizBtns;
        [SerializeField] GameObject permeablePic;
        [SerializeField] GameObject nonpermeablePic;
        [SerializeField] GameObject closeQuizBtn;
        [SerializeField] GameObject quizEndText;

        // Start is called before the first frame update
        void Awake()
        {
            quizText.text = "Trees should be planted on: ";
            quizBtns.SetActive(true);
            permeablePic.SetActive(false);
            nonpermeablePic.SetActive(false);
            closeQuizBtn.SetActive(false);
            quizEndText.SetActive(false);
        }

        
        public void PermeableAnswer()
        {
            quizBtns.SetActive(false);
            quizText.text = "Yes! Permeable surfaces, like a grassy yard, let water through so we should plant trees there!";
            permeablePic.SetActive(true);
            closeQuizBtn.SetActive(true);
        }

        public void NonPermeableAnswer()
        {
            quizBtns.SetActive(false);
            quizText.text = "Not quite, non-permeable surfaces, like concrete, don't let water through. We should plant trees on permeable surfaces, like grass.";
            permeablePic.SetActive(true);
            closeQuizBtn.SetActive(true);
        }



        public void CloseQuiz()
        {
            //UrbanForestryQuestManager.GetInstance().CurrentState = UrbanForestryQuestManager.GameState.Tutorial;
            quizBtns.SetActive(false);
            permeablePic.SetActive(false);
            nonpermeablePic.SetActive(false);
            closeQuizBtn.SetActive(false);
            quizEndText.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
