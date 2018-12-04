﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GR19
{
    public class GameManager : MonoBehaviour
    {

        public GameObject ui;
        public Slider slider;
        public bool tuto = true;


        // Use this for initialization
        void Start()
        {
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
        }
    }
}
