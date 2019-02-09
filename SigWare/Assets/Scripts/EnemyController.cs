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


        public float enemySpeed;
        public float enemyAcceleration;
        public float enemyRagingSpeed;

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

            if (raging == true)
            {
                anim.SetBool("isChasing", true);
                RagingNurseLvl1();
            }

            if (raging == false)
            {
                if (!enemy.pathPending && enemy.remainingDistance < 0.5f)
                    GotoNextPoint();

                if (playerMovement.isCharging == true)
                {
                    enemy.destination = player.transform.position;
                    //anim.SetBool("isChasing", true);
                }
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

        public void NurseAppear()
        {
                enemy.speed = enemySpeed;
                enemy.acceleration = enemyAcceleration;
        }

        public void RagingNurseLvl1()
        {
            enemy.speed = enemyRagingSpeed;
            if(nurseActive == true)
            {
                enemy.destination = player.transform.position;
            }
        }
    }
}
