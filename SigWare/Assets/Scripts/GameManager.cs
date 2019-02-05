using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GR19
{
    public class GameManager : MonoBehaviour
    {

        public GameObject ui;
        public Slider slider;
        public bool tuto = true;
        public GameObject defeatText;
        public GameObject victoryText;
        public Image batteryImage;
        public bool debug;

        public GameObject nurse;
        public GameObject timer;


        public Animator tutoAnim;

        TimerController timeControl;

        public PlayerMovement playerMovement;
        public EnemyController enemyController;


        void Start()
        {
            if (defeatText != null)
            {
                defeatText.SetActive(false);
            }
        }

        void Update()
        {
            if (batteryImage.fillAmount >= 0.85f)     //Si amount batteryImage > 85%
            {
                tutoAnim.SetInteger("State", 1);
                nurse.SetActive(true);
                enemyController.NurseAppear();
                StartCoroutine(TimerAppear());
            }

            if (debug == true)
            {
                batteryImage.fillAmount = 100;
            }

            if (batteryImage.fillAmount == 0)
            {
                defeatText.SetActive(true);
                Time.timeScale = 0;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }

        }

        private IEnumerator TimerAppear()
        {
            timer.SetActive(true);
            yield return new WaitForSeconds(1);
        }

        public void Victory()
        {
            if (batteryImage.fillAmount > 0)
            {
                victoryText.SetActive(true);
                Time.timeScale = 0;
                //Debug.Log("C'est gagné");
            }
            else
            {
                defeatText.SetActive(true);
                Time.timeScale = 0;
            }
        }


    }
}