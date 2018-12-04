using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GR19
{
    public class TimerController : MonoBehaviour
    {

        public Slider slider;
        public float time = 30f;

        void Start()
        {
            slider.maxValue = 30f;
            slider.minValue = 0f;
        }

        void Update()
        {
            time -= Time.deltaTime;
            slider.value = time;
        }
    }
}
