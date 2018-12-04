using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GR19
{

    public class EnemyController : MonoBehaviour {

        public NavMeshAgent enemy;
        public Transform nurseTransform;
        public GameObject player;
        private float fixedRotation = 5f;

        public Transform[] points;
        private int destPoint = 0;

        PlayerMovement playerMovement;

        public Animator anim;




        void Start() {
            nurseTransform = transform;
            enemy = GetComponent<NavMeshAgent>();
            GotoNextPoint();
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        }

        void GotoNextPoint()
        {
            // Return s'il n'y a pas de point de Setup
            if (points.Length == 0)
                return;

            // Déplace la nurse vers la destination
            enemy.destination = points[destPoint].position;

            // Choisi le prochain point dans l'array
            // Boucle jusqu'au début 
            destPoint = (destPoint + 1) % points.Length;
        }

        void Update() {
            //enemy.destination = player.transform.position;        //Fait que la nurse chasse le player
            nurseTransform.eulerAngles = new Vector3(nurseTransform.eulerAngles.x, fixedRotation, nurseTransform.eulerAngles.z);

            if (!enemy.pathPending && enemy.remainingDistance < 0.5f)
                GotoNextPoint();

            if (playerMovement.isCharging == true)
            {
                enemy.destination = player.transform.position;
                Debug.Log("Chasse du joueur");
                anim.SetBool("isChasing", true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && playerMovement.isCharging == true)
            {
                playerMovement.Respawn();
                playerMovement.ResetPlayer();
                anim.SetBool("isChasing", false);
                playerMovement.batteryImage.fillAmount = playerMovement.batteryImage.fillAmount - playerMovement.respawnMalus;
            }
        }
    }
}
