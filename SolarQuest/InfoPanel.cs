using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SolarQuest
{
    public class InfoPanel : MonoBehaviour
    {

        Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void ShowInfoPanel()
        {
            animator.SetBool("IsOnScreen", true);
        }

        public void HideInfoPanel()
        {
            animator.SetBool("IsOnScreen", false);
        }
    }
}

