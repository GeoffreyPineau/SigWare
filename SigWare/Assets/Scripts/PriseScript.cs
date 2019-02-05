using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GR19
{
    public class PriseScript : MonoBehaviour
    {

        public GameObject cable;
        public Animator pluganim;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(true);
                pluganim.SetInteger("State", 1);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(false);
                pluganim.SetInteger("State", 2);
            }
        }
    }
}
