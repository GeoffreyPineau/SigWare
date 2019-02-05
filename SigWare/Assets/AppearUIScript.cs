using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearUIScript : MonoBehaviour {

    public GameObject player;
    public GameObject nurse;
    public GameObject battery;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)     //Détecte la collision avec la zone de la prise
    {
        if (other != nurse)
        {
            battery.SetActive(true);
        }
    }
}
