using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanForestryQuest
{
    public class MapArrow : MonoBehaviour
    {
        Animator animator;
        [SerializeField] float leftEdge = 17f;
        [SerializeField] float rightEdge = 70f;
        [SerializeField] float edgePanSpeed = 10f;

        public CameraController cameraController;
        private bool pressed;
        private float currentEdgePanSpeed;
        public GameObject currentCam;

        bool doneLoading;

        // Start is called before the first frame update
        void Awake()
        {
            Initialize();
        }

        public void Initialize(){
            animator = GetComponent<Animator>();
            pressed = false;
            doneLoading = true;
            Debug.Log("Initialize arrows");
            Debug.Log(cameraController);
        }

        // Update is called once per frame
        void Update()
        {
            if (pressed && doneLoading)
            {
                /*
                if(cameraController == null){
                    cameraController = CameraController.GetInstance();
                    return;
                }*/
                cameraController.MovementEnabled = false;                
                currentCam.transform.position = new Vector3(Mathf.Clamp(currentCam.transform.position.x + currentEdgePanSpeed * Time.deltaTime, leftEdge, rightEdge), currentCam.transform.position.y, currentCam.transform.position.z);
            }
        }

        public void OnPointerEnterArrow()
        {            
            //cameraController = CameraController.GetInstance();
            //currentCam = cameraController.currentCam;
            currentEdgePanSpeed = currentCam.name.Equals("Main Camera") ? edgePanSpeed : edgePanSpeed * -1;
        }

        public void ArrowPressed()
        {
            animator.SetBool("pressed", true);
            pressed = true;
        }

        public void ArrowReleased()
        {
            animator.SetBool("pressed", false);
            pressed = false;
            cameraController.MovementEnabled = true;
        }
       
    }
}

