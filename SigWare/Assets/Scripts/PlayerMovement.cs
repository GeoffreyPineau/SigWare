using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace GR19
{
    public class PlayerMovement : MonoBehaviour
    {

        public Transform playerTransform;

        public float fixedRotation = 5f;

        public Camera cam;

        public NavMeshAgent agent;

        public Slider batterySlider;

        [Range(0.001f, 0.01f)]
        public float decreasingValue;

        public float speed;

        public bool canMove;

        private int plug = 0;

        public float batteryCharge = 0.7f;

        public float batteryValue = 0.7f;

        [Range(10f, 1000f)]
        public float chargingModifier = 10f;

        public GameObject respawnPoint;

        public bool canPlug;

        public Animator anim;

        public LineRenderer lineRenderer;

        public Transform plugPosition;

        public bool isCharging;


        public void Start()
        {
            batterySlider.value = batteryCharge;
            playerTransform = transform;

        }

        void Update()
        {
            PlugUnplug();       //Fonction stop/démarrer
            Charging();         //Fonction rechargement

            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, fixedRotation, playerTransform.eulerAngles.z);

            batterySlider.value = batterySlider.value - decreasingValue;     //Diminution du slider

            if (canMove == true && plug == 0)
            {
                PlayerMovementNav();
                Debug.Log("is moving");
            }
        }

        private void PlugUnplug()
        {
            if (Input.GetMouseButtonDown(0) && plug == 0 && canPlug == true)       //Clic gauche pour Stop
            {
                canMove = false;
                Debug.Log("plugged");
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                plug = 1;
                anim.SetBool("isCharging", true);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, plugPosition.position);
                isCharging = true;
            }

            if (Input.GetMouseButtonDown(1) && plug == 1)       //Clic droit pour Démarrer
            {
                canMove = true;
                plug = 0;
                Debug.Log("unplugged");
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                anim.SetBool("isCharging", false);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(0, plugPosition.position);
                isCharging = false;
            }

        }

        private void PlayerMovementNav()        //Mouvement du Player
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        private void Charging()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (canMove == false)
            {
                batteryValue = Mathf.Abs(mouseX + mouseY) / chargingModifier;      //Retourne la valeur positive du calcul
                //Debug.Log(batteryValue);
                ValueChangeCheck();                             // Appel la maj du Slider
            }
        }

        public void ResetPlayer()
        {
            canMove = true;
            plug = 0;
            Debug.Log("Reset");
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            anim.SetBool("isCharging", false);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(0, plugPosition.position);
            isCharging = false;
        }

        public void ValueChangeCheck()      //MAJ du slider
        {
            //Debug.Log(batterySlider.value);
            batterySlider.value = batterySlider.value + batteryValue;
        }

        private void OnTriggerEnter(Collider other)     //Détecte la collision avec la zone de la prise
        {
            canPlug = true;
        }

        private void OnTriggerExit(Collider other)     //Détecte la sortie de la zone de la prise
        {
            canPlug = false;
        }

        public void Respawn()       //Respawn player
        {
            this.transform.position = respawnPoint.transform.position;
        }
    }
}
