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


        // Use this for initialization
        void Start()
        {
            t = transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 batteryPos = Camera.main.WorldToScreenPoint(this.transform.position);
            batterySlide.transform.position = batteryPos;

            Vector3 plugPos = Camera.main.WorldToScreenPoint(this.transform.position);
            plugObject.transform.position = batteryPos;
            t.eulerAngles = new Vector3(t.eulerAngles.x, fixedRotation, t.eulerAngles.z);
        }
    }
}
