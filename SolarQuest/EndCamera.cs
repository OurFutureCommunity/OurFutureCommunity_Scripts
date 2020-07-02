using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarQuest
{
    public class EndCamera : MonoBehaviour
    {
        [SerializeField] float verticalSpeed;
        [SerializeField] Vector3 destination;
        [SerializeField] GameObject proceedButton;

        private bool isMoving;
        private bool reachedDestination;

        private void Start()
        {
            proceedButton.SetActive(false);
        }
        void Update()
        {
            if (isMoving)
            {
                reachedDestination = (transform.position.y >= destination.y);
                if (!reachedDestination)
                {
                    transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
                }
                else
                {
                    proceedButton.SetActive(true);
                    isMoving = false;
                }
            }
        }

        public void ZoomOutCamera()
        {
            isMoving = true;
        }
    }

}
