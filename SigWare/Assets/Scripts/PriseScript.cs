﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GR19
{
    public class PriseScript : MonoBehaviour
    {

        public GameObject cable;
        public Animator pluganim;
        public Animator plugUIAnim;

        public ParticleSystem particle;

        public Animator sickLitAnim;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(true);
                pluganim.SetInteger("State", 1);
                plugUIAnim.SetBool("isCharging", true);
                sickLitAnim.SetBool("girlIsHere", true);
                particle.Emit(12);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(false);
                pluganim.SetInteger("State", 2);
                plugUIAnim.SetBool("isCharging", false);
                sickLitAnim.SetBool("girlIsHere", false);
            }
        }
    }
}
