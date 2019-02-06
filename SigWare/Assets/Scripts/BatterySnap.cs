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

        public Transform t;
        public float fixedRotation = 5;

        public GameObject batteryLow;


        // Use this for initialization
        void Start()
        {
            t = transform;
        }

        // Update is called once per frame
        void Update()
        {
            if(batterySlide != null)
            {
                Vector3 batteryPos = Camera.main.WorldToScreenPoint(this.transform.position);
                batterySlide.transform.position = batteryPos;
            }

            if(plugObject != null)
            {
                Vector3 plugPos = Camera.main.WorldToScreenPoint(this.transform.position);
                plugObject.transform.position = plugPos;
                t.eulerAngles = new Vector3(t.eulerAngles.x, fixedRotation, t.eulerAngles.z);
            }
            
            if(batteryLow != null)
            {
                Vector3 batteryLowPos =this.transform.position;
                batteryLow.transform.position = batteryLowPos;
            }
        }
    }
}
