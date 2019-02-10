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
        public int ragingTiming;
        public EnemyController enemyController;

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
            }

            if(slider.value  <= ragingTiming)
            {
                enemyController.RagingNurseLvl1();
                enemyController.raging = true;
            }
        }
    }
}
