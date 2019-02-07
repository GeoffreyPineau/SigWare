using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

namespace GR19
{

    public class EnemyController : MonoBehaviour {

        public NavMeshAgent enemy;
        public Transform nurseTransform;
        public GameObject player;
        private float fixedRotation = 5f;

        public Transform[] points;
        private int destPoint = 0;

        public PlayerMovement playerMovement;

        public Animator anim;

        public bool raging;
        public bool nurseActive = true;

        void Start() {
            nurseTransform = transform;
            enemy = GetComponent<NavMeshAgent>();
            GotoNextPoint();
            playerMovement.isCharging = false;
        }

        void GotoNextPoint()
        {
            if (points.Length == 0)     // Return s'il n'y a pas de point de Setup
                return;

            enemy.destination = points[destPoint].position;     // Déplace la nurse vers la destination
           
            destPoint = (destPoint + 1) % points.Length;           // Choisi le prochain point dans l'array                                                    // Boucle jusqu'au début 
        }

        void Update() {

            if(raging == false)
            {
                if (!enemy.pathPending && enemy.remainingDistance < 0.5f)
                    GotoNextPoint();

                if (playerMovement.isCharging == true)
                {
                    enemy.destination = player.transform.position;
                    //anim.SetBool("isChasing", true);
                }
            }
            

            if(raging == true)
            {
                anim.SetBool("isChasing", true);
                RagingNurseLvl1();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(playerMovement.isCharging == true && other == player)
            {
                playerMovement.Respawn();
                anim.SetBool("isChasing", false);
                GotoNextPoint();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other == player)
            {
                Debug.Log("Collision player");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (playerMovement.isCharging == false && other == player)
            {
            }
        }

        public void NurseAppear()
        {
            enemy.speed = 3;
            enemy.acceleration = 15;
        }

        public void RagingNurseLvl1()
        {
            enemy.speed = 6f;
            enemy.angularSpeed = 100000;
            if(nurseActive == true)
            {
                enemy.destination = player.transform.position;
            }
        }
    }
}
