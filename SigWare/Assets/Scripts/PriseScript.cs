using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GR19
{
    public class PriseScript : MonoBehaviour
    {

        public GameObject cable;
        public Animator pluganim;
        public Animator plugUIAnim;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(true);
                pluganim.SetInteger("State", 1);
                plugUIAnim.SetBool("isCharging", true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(false);
                pluganim.SetInteger("State", 2);
                plugUIAnim.SetBool("isCharging", false);
            }
        }
    }
}
