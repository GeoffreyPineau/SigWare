using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GR19
{
    public class TimerController : MonoBehaviour
    {
        public GameManager gameManager;
        public Slider slider;
        public float time = 20f;

        void Start()
        {
            slider.maxValue = 20f;
            slider.minValue = 0f;
        }

        void Update()
        {
            time -= Time.deltaTime;
            slider.value = time;

            if (slider.value <= 0)
            {
                gameManager.Victory();
                Debug.Log("Victory Time Controller Script !");
            }
        }
    }
}
