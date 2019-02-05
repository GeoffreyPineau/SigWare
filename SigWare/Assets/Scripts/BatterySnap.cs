using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GR19
{
    public class BatterySnap : MonoBehaviour
    {

        public Slider batterySlide;
        public GameObject plugObject;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 batteryPos = Camera.main.WorldToScreenPoint(this.transform.position);
            batterySlide.transform.position = batteryPos;

            Vector3 plugPos = Camera.main.WorldToScreenPoint(this.transform.position);
            plugObject.transform.position = batteryPos;
        }
    }
}
