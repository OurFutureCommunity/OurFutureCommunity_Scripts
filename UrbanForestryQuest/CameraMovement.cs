using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UrbanForestryQuest
{
    public class CameraMovement : MonoBehaviour
    {
        CameraController controller;

        // Target
        Transform target;
        Vector3 targetOffset;
        [SerializeField] float targetDistance = 5f;

        // Camera movement adjustment
        [SerializeField] float zoomRate = 10.0f;
        [SerializeField] float panSpeed = 0.3f;
        [SerializeField] float zoomDampening = 5.0f;

        // Boundary of camera
        [SerializeField] float leftEdge = 17f;
        [SerializeField] float rightEdge = 70f;
        [SerializeField] float yAxis = 40f;
        [SerializeField] float zAxis = 11f;

        private float xDeg = 0.0f;
        private float yDeg = 0.0f;
        private float currentDistance;
        private float desiredDistance;
        private Quaternion rotation;
        private Vector3 position;

        private Vector3 firstPos;
        private Vector3 secondPos;
        private Vector3 delta;
        private Vector3 lastOffset;

        private Vector3 origCameraPos;
        private Quaternion origCameraRot;

        [SerializeField] float screenWidth;
        [SerializeField] float screenBoundary = 20;
        public float edgePanSpeed = 10;

        void Start()
        {
            Init();
            screenWidth = Screen.width;
        }
        void OnEnable() { Init(); }

        public void Init()
        {
            // Create a temporary target at 'distance' from the cameras current viewpoint
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * targetDistance);
            target = go.transform;

            targetDistance = Vector3.Distance(transform.position, target.position);
            currentDistance = targetDistance;
            desiredDistance = targetDistance;

            // Set current rotations as starting points.
            position = transform.position;
            rotation = transform.rotation;

            origCameraPos = transform.position;
            origCameraRot = transform.rotation;

            xDeg = Vector3.Angle(Vector3.right, transform.right);
            yDeg = Vector3.Angle(Vector3.up, transform.up);

            controller = GetComponentInParent<CameraController>();
            
        }


        private Vector3 touchStart;
        public float groundZ = 0;

        void FixedUpdate()
        {
            if (controller.MovementEnabled)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchStart = GetWorldPosition(groundZ);
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 direction = touchStart - GetWorldPosition(groundZ);
                    transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x + direction.x, leftEdge, rightEdge),
                        Mathf.Clamp(transform.position.y + direction.y, yAxis, yAxis),
                        Mathf.Clamp(transform.position.z + direction.z, zAxis, zAxis));
                }

            }
            else if (!controller.MovementEnabled && Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > screenWidth - screenBoundary)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x + edgePanSpeed * Time.deltaTime, leftEdge, rightEdge), transform.position.y, transform.position.z);
                }
                if (Input.mousePosition.x < 0 + screenBoundary)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x - edgePanSpeed * Time.deltaTime, leftEdge, rightEdge), transform.position.y, transform.position.z);
                }
            }

        }


        private Vector3 GetWorldPosition(float z)
        {
            Ray mousePos = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            Plane ground = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            ground.Raycast(mousePos, out distance);
            return mousePos.GetPoint(distance);
        }

    }


}

