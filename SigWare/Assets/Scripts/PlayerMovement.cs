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

        public bool isCharging;
        public Animator batteryAnim;

        [Range(0.01f, 1f)]
        public float respawnMalus;


        public void Start()
        {
            batteryImage.fillAmount = batteryCharge;
            playerTransform = transform;
            

        }

        void Update()
        {
            PlugUnplug();       //Fonction stop/démarrer
            Charging();         //Fonction rechargement

            playerTransform.eulerAngles = new Vector3(playerTransform.eulerAngles.x, fixedRotation, playerTransform.eulerAngles.z);

            batteryImage.fillAmount = batteryImage.fillAmount - decreasingValue;     //Diminution du slider

            if (canMove == true && plug == 0)
            {
                if(Input.GetKey("mouse 0"))
                {
                    StartCoroutine(Dash());
                }
                PlayerMovementNav();
                Debug.Log("is moving");
            }


        }

        private IEnumerator Dash()
        {
            agent.speed = thrust;       //Set la speed du navMeshAgent a la valeur de thrust
            Debug.Log("DASH");
            yield return new WaitForSeconds(0.12f);
            agent.speed = 6;            // Reset la valeur du navMeshAgent.speed
        }

        private void PlugUnplug()
        {
            if (Input.GetMouseButtonDown(0) && plug == 0 && canPlug == true)       //Clic gauche pour Stop
            {
                canMove = false;
                Debug.Log("plugged");
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                plug = 1;
                pointLightAnim.SetBool("isCharging", true);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, plugPosition.position);
                isCharging = true;
                batteryAnim.SetBool("isCharging", true);
            }

            if (Input.GetMouseButtonDown(1) && plug == 1)       //Clic droit pour Démarrer
            {
                canMove = true;
                plug = 0;
                Debug.Log("unplugged");
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
                agent.SetDestination(hit.point);
            }
        }

        private void Charging()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (canMove == false)
            {
                batteryImage.fillAmount = batteryImage.fillAmount + 0.01f; //(Mathf.Abs(mouseX + mouseY) / chargingModifier) ;      //Retourne la valeur positive du calcul
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
            pointLightAnim.SetBool("isCharging", false);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(0, plugPosition.position);
            isCharging = false;
            batteryAnim.SetBool("isCharging", false);
        }

        public void ValueChangeCheck()      //MAJ du slider
        {
            //Debug.Log(batterySlider.value);
            batteryImage.fillAmount = batteryImage.fillAmount + 0.01f ;
        }

        private void OnTriggerEnter(Collider other)     //Détecte la collision avec la zone de la prise
        {
            canPlug = true;
            isCharging = true;
            ValueChangeCheck();
        }
        private void OnTriggerStay(Collider other)     //Détecte la collision avec la zone de la prise
        {
            canPlug = true;
            isCharging = true;
            ValueChangeCheck();
            Charging();
        }

        private void OnTriggerExit(Collider other)     //Détecte la sortie de la zone de la prise
        {
            canPlug = false;
            isCharging = false;
        }

        public void Respawn()       //Respawn player
        {
            this.transform.position = respawnPoint.transform.position;
        }
    }
}
