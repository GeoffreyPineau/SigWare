using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR19
{
    public class RandomChildMaterial : MonoBehaviour
    {

        public Material[] randomMaterials;
        public GameObject sickKid;
        public AudioSource ouchCartoon;
        public AudioSource rollingChair;

        public void ColorKid()
        {

                sickKid.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
            /*foreach (GameObject kid in sickKid)
            { 

            }*/
        }

        public void PlaySound()
        {
            ouchCartoon.Play();
        }

        public void PlaySoundChair()
        {
            rollingChair.Play();
        }
    }
}

