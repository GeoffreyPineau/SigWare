﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

namespace GR19
{
    public class PlayerMovement : MonoBehaviour
    {

        public GameObject player;
        public GameObject nurse;
        public Transform playerTransform;

        public Rigidbody rb;

        public float fixedRotation = 5f;

        public Camera cam;

        public NavMeshAgent agent;

        public Slider batterySlider;
        public Image batteryImage;

        [Range(0.001f, 0.01f)]
        public float decreasingValue;

        public float speed;

        public float thrust;

        public bool canMove;

        private bool dashing;

        private int plug = 0;

        public float batteryCharge = 0.7f;

        public float batteryValue = 0.7f;

        [Range(100f, 500f)]
        public float chargingModifier;

        public GameObject respawnPoint;

        public bool canPlug;

        public Animator pointLightAnim;

        public Animator nurseAnim;

        public LineRenderer lineRenderer;

        public Transform plugPosition;

        public bool isCharging = false;
        public Animator batteryAnim;

        [Range(0.01f, 1f)]
        public float respawnMalus;

        public ParticleSystem dustDash;

        public GameObject ground;

        public GameObject plug1;
        public Animator plugAnim;

        public PostProcessVolume mainCam;
        Vignette vignette;

        public bool nurseCollided = false;
        public float vignettage;

        public GameObject defeatText;


        public void Start()
        {
            batteryImage.fillAmount = batteryCharge;
            batteryImage.fillAmount = 0.7f;
            playerTransform = transform;
            isCharging = false;


            vignette = ScriptableObject.CreateInstance<Vignette>();
            vignette.enabled.Override(true);
            vignette.intensity.Override(0f);
            mainCam = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        }

        void Update()
        {
            //PlugUnplug();       //Fonction stop/démarrer
            //Charging();         //Fonction rechargement
            //Debug.Log(isCharging);
            //playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, fixedRotation, playerTransform.eulerAngles.z);

            batteryImage.fillAmount = batteryImage.fillAmount - decreasingValue;     //Diminution du slider

            if (canMove == true && plug == 0)
            {
                if(Input.GetKey("mouse 0"))
                {
                    StartCoroutine(Dash());
                    dashing = true;
                }
                PlayerMovementNav();
            }

            if(nurseCollided == true)
            {
                vignette.intensity.value = vignettage + 0.01f;
                vignettage = vignettage + 0.01f;
                Debug.Log(vignettage);
            }

            if(vignettage >= 1)
            {
                defeatText.SetActive(true);
                Time.timeScale = 0;
            }
            
        }

        private IEnumerator Dash()
        {
            agent.speed = thrust;       //Set la speed du navMeshAgent a la valeur de thrust
            yield return new WaitForSeconds(0.12f);
            dashing = false;
            agent.speed = 4.5f ;            // Reset la valeur du navMeshAgent.speed
            dustDash.Play();
        }

        private void PlugUnplug()
        {
            if (Input.GetMouseButtonDown(0) && plug == 0 && canPlug == true)       //Clic gauche pour Stop
            {
                //canMove = false;
                //Debug.Log("plugged");
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                plug = 1;
                pointLightAnim.SetBool("isCharging", true);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, plugPosition.position);
                //isCharging = true;
                batteryAnim.SetBool("isCharging", true);
            }

            if (Input.GetMouseButtonDown(1) && plug == 1)       //Clic droit pour Démarrer
            {
                //canMove = true;
                plug = 0;
                //Debug.Log("unplugged");
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                pointLightAnim.SetBool("isCharging", false);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(0, plugPosition.position);
                isCharging = false;
                nurseAnim.SetBool("isChasing", false);
                batteryAnim.SetBool("isCharging", false);
            }

        }

        private void PlayerMovementNav()        //Mouvement du Player
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject != player)
                {
                    if(dashing == false)
                    {
                        agent.speed = 4.5f;
                        agent.SetDestination(hit.point);
                    }
                   
                }
                if (hit.collider.gameObject == player)
                {
                    agent.speed = 0;
                }
            }
        }

        private void Charging()
        {

            batteryImage.fillAmount = batteryImage.fillAmount + 0.001f; //(Mathf.Abs(mouseX + mouseY) / chargingModifier) ;      //Retourne la valeur positive du calcul
            //Debug.Log("charging");
            
            ValueChangeCheck();                             // Appel la maj du Slider   

            /*if (canMove == false)
            {
                batteryImage.fillAmount = batteryImage.fillAmount + 0.01f; //(Mathf.Abs(mouseX + mouseY) / chargingModifier) ;      //Retourne la valeur positive du calcul
                //Debug.Log(batteryValue);
                ValueChangeCheck();                             // Appel la maj du Slider
            }*/
        }

        /*public void ResetPlayer()
        {
            canMove = true;
            plug = 0;
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            pointLightAnim.SetBool("isCharging", false);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(0, plugPosition.position);
            isCharging = false;
            batteryAnim.SetBool("isCharging", false);
        }*/

        public void ValueChangeCheck()      //MAJ du slider
        {
            //Debug.Log(batterySlider.value);
            batteryImage.fillAmount = batteryImage.fillAmount + 0.001f;
            isCharging = true;
            //Debug.Log("Nurse chasing");
        }

        private void OnTriggerEnter(Collider other)     //Détecte la collision avec la zone de la prise
        {
            if(other != nurse && other != ground)
            {
                canPlug = true;
                //isCharging = true;
                //ValueChangeCheck();
                Charging();
                //Debug.Log("collider1");
                if(other == plug1)
                {
                    plugAnim.SetInteger("State", 1);
                    //Debug.Log("Anim plug material");
                }
            }


            
        }
        private void OnTriggerStay(Collider other)     //Détecte la collision avec la zone de la prise
        {
            if (other != nurse && other != ground)
            {
                canPlug = true;
                //isCharging = true;
                ValueChangeCheck();
                Charging();
                //Debug.Log("collider2");
            }

            if(other.name == "DamageCollider")
            {
                nurseCollided = true;
                Debug.Log("nurse collided = true");
            }
                
        }

        private void OnTriggerExit(Collider other)     //Détecte la sortie de la zone de la prise
        {
            canPlug = false;
            isCharging = false;

            if(other.name == "DamageCollider")
            {
                nurseCollided = false;
                Debug.Log("nurse collided = false");
                vignette.intensity.value = 0;
                vignettage = 0;

            }
        }

        public void Respawn()       //Respawn player
        {
            this.transform.position = respawnPoint.transform.position;
        }
    }
}
