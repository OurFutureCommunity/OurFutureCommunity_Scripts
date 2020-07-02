using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public bool movementEnabled = false;
    Vector3 touchStart;
    [SerializeField] float zoomOutMin = 7;
    [SerializeField] float zoomOutMax = 29;
    [SerializeField] float zoomSpeed = 1f;

    [SerializeField] float leftEdge = -34f;
    [SerializeField] float rightEdge = 62f;

    [SerializeField] float maxY = 49f;
    [SerializeField] float minY = 45f;

    [SerializeField] float maxZ = 10f;
    [SerializeField] float minZ = 2.5f; 

    void Start()
    {
        
    }


    void Update()
    {
        if (movementEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * zoomSpeed);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x + direction.x, leftEdge, rightEdge),
                                                            Mathf.Clamp(Camera.main.transform.position.y + direction.y, minY, maxY),
                                                            Mathf.Clamp(Camera.main.transform.position.z + direction.z, minZ, maxZ));
            }
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
        if(GlobalControl.Instance.solarQuestPlayed && !movementEnabled)
        {
            movementEnabled = true;
        }

    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
