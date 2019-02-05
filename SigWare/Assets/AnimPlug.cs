using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlug : MonoBehaviour {

    public Animator plugAnim;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)     //Détecte la collision avec la zone de la prise
    {
        if (other == player)
        {
            Debug.Log("changement material");
            plugAnim.SetInteger("State", 1);
        }
    }

    void OnTriggerStay(Collider other)     //Détecte la collision avec la zone de la prise
    {
        if (other == player)
        {
            plugAnim.SetInteger("State", 2);
        }
    }

    void OnTriggerExit(Collider other)     //Détecte la collision avec la zone de la prise
    {
        if (other == player)
        {
            plugAnim.SetInteger("State", 3);
        }
    }
}
