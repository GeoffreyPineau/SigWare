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

        TimerController timeControl;



        // Use this for initialization
        void Start()
        {
            if(defeatText != null)
            {
                defeatText.SetActive(false);
            }

            if (tuto)       //Si tuto, jeu en pause
            {
                Time.timeScale = 0;
            }


        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))     //Fin du tuto, temps normal
            {
                Time.timeScale = 1;
                ui.SetActive(false);
            }

            if(debug == true)
            {
                batteryImage.fillAmount = 100;
                if(Input.GetKeyDown (KeyCode.R))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    Debug.Log("Reload");
                }
            }

            if(batteryImage.fillAmount == 0)
            {
                defeatText.SetActive(true);
                Time.timeScale = 0;
            }


        }

        public void Victory()
        {
            if (batteryImage.fillAmount > 0)
            {
                victoryText.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("C'est gagné");
            }
            else
            {
                defeatText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
