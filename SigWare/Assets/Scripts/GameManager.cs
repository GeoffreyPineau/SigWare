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

        public GameObject batteryLowImage;
        public GameObject battery;
        public GameObject player;
        bool batteryLowAppeared = false;

        public GameObject nurse;

        public GameObject timer;


        public Animator tutoAnim;

        TimerController timeControl;

        public PlayerMovement playerMovement;
        public EnemyController enemyController;
        public BatterySnap batterySnap;


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
                enemyController.nurseActive = true;
                enemyController.NurseAppear();
                StartCoroutine(TimerAppear());
            }

            if (debug == true)
            {
                batteryImage.fillAmount = 100;
            }

            if (batteryImage.fillAmount == 0)
            {
                //Time.timeScale = 0;
                Defeat();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }

            if (batteryImage.fillAmount < 0.25 && batteryLowAppeared == false)
            {
                batteryLowImage.SetActive(true);
                batteryLowAppeared = true;
                batterySnap.batteryLowAppearedSnap = true;
                StartCoroutine(BatteryLowDisapeared());
            }

            if(batteryImage.fillAmount > 0.25)
            {
                batteryLowImage.SetActive(false);
            }

        }

        private IEnumerator BatteryLowDisapeared()
        {
            yield return new WaitForSeconds(1);
            batterySnap.batteryLowAppearedSnap = false;
            batteryLowAppeared = false;
        }

        private IEnumerator TimerAppear()
        {
            timer.SetActive(true);
            yield return new WaitForSeconds(1);
        }

        public void Defeat()
        {
            StartCoroutine(DefeatScreen());
        }

        public void Victory()
        {
            if (batteryImage.fillAmount > 0)
            {
                //Debug.Log("C'est gagné");
                StartCoroutine(VictoryScreen());

            }
            else
            {
                //Time.timeScale = 0;
                Defeat();
            }
        }

        private IEnumerator VictoryScreen()
        {
            enemyController.nurseActive = false;
            player.SetActive(false);
            battery.SetActive(false);
            victoryText.SetActive(true);
            nurse.SetActive(false);
            yield return new WaitForSeconds(3f);
            int callingLevelManager = 0;
            if (callingLevelManager == 0)
            {
                callingLevelManager++;
#if SIGWARE
      LevelManager.Instance.Lose();
#endif
                Debug.Log("call WIN" + callingLevelManager);
            }

        }

        private IEnumerator DefeatScreen()
        {
            defeatText.SetActive(true);
            battery.SetActive(false);
            player.SetActive(false);
            nurse.SetActive(false);
            yield return new WaitForSeconds(3f);
            int callingLevelManager = 0;
            if (callingLevelManager == 0)
            {
                callingLevelManager++;
#if SIGWARE
      LevelManager.Instance.Lose();
#endif
                Debug.Log("call LOSE" + callingLevelManager);
            }

        }
    }
}