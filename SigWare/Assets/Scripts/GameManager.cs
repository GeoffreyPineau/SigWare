using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject ui;
    public Slider slider;
    public bool tuto = true;


	// Use this for initialization
	void Start () {
        if (tuto)
            {
            Time.timeScale = 0;
            }
        

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            ui.SetActive(false);
        }
	}
}
