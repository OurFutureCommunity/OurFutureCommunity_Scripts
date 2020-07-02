using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SolarQuest
{
    public class SolarQuestQuiz : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI quizText;
        [SerializeField] GameObject compassButtons;
        [SerializeField] GameObject continueButton;
        [SerializeField] int maxAttempts = 3;

        private int currentAttempts;


        private void Start()
        {
            currentAttempts = 0;
            continueButton.SetActive(false);
        }
        private void Update()
        {
            if (currentAttempts == maxAttempts)
            {
                NoAttemptsLeft();
            }
        }

        public void SelectedSouth()
        {
            compassButtons.SetActive(false);
            quizText.text = "Great! You've got it! Panels should be placed on the south-facing roofs in Vancouver for maximum solar potential.";
            EndQuiz();

        }

        public void SelectedWrongAnswer()
        {
            currentAttempts++;
            if (currentAttempts == 1)
            {
                quizText.text = "Not quite... Try again.";
            }
            else if (currentAttempts == 2)
            {
                quizText.text = "You have 1 last chance.  Click 'back' to read the information again.";
            }

        }

        private void NoAttemptsLeft()
        {
            compassButtons.SetActive(false);
            quizText.text = "Panels should be placed on the south-facing roofs in Vancouver for maximum solar potential.";
            EndQuiz();
        }

        private void EndQuiz()
        {
            continueButton.SetActive(true);
        }
    }

}
