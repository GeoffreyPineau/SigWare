﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GR19
{
    public class PriseScript : MonoBehaviour
    {

        public GameObject cable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(false);
            }
        }
    }
}
