using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SolarQuest
{
    public class SkyboxRotator : MonoBehaviour
    {
        public Slider sunSlider;

        private float xInit = 10f;
        private float yInit = -24f;
        private float zInit = 2f;

        private float xFinal = 10f;
        private float yFinal = -157.377f;
        private float zFinal = -170.264f;

        float x, y, z;

        // 40 to 160
        private void Start()
        {
            sunSlider.onValueChanged.AddListener(delegate { OnSliderChange(); });
            transform.Rotate(xInit, yInit, zInit);
        }

        public void OnSliderChange()
        {

            x = (xInit - xFinal) * sunSlider.normalizedValue;
            y = (yInit - yFinal) * sunSlider.normalizedValue;
            z = (zInit - zFinal) * sunSlider.normalizedValue;
            transform.localEulerAngles = new Vector3(xInit - x, yInit - y, zInit - z);
        }
    }

}
