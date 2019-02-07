using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR19
{
    public class RandomChildMaterial : MonoBehaviour
    {

        public Material[] randomMaterials;
        public GameObject sickKid;

        public void ColorKid()
        {

                sickKid.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
            /*foreach (GameObject kid in sickKid)
            { 

            }*/
        }
    }
}

