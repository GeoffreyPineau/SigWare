using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GR19
{
    public class PriseScript : MonoBehaviour
    {

        public GameObject cable;
        public Animator pluganim;
        public Animator plugUIAnim;


        public ParticleSystem particle;
        public int numberParticle;
        public GameObject clicSound;
        public AudioSource fireSound;
        bool soundPlayed = false;
        bool particleEmit = false;
        public float fireVolume = 0.4f;
        public float fireVolumeStart = 0.1f;


        public Animator sickLitAnim;

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(true);
                pluganim.SetInteger("State", 1);
                plugUIAnim.SetBool("isCharging", true);
                sickLitAnim.SetBool("girlIsHere", true);
                if(particleEmit == false)
                {
                    particle.Emit(numberParticle);
                    particleEmit = true;
                    Debug.Log("EmitGhost");
                    if(soundPlayed == false)
                    {
                        clicSound.SetActive(true);
                        soundPlayed = true;
                        fireSound.volume = fireVolume;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.name == "Player")
            {
                cable.SetActive(false);
                pluganim.SetInteger("State", 2);
                plugUIAnim.SetBool("isCharging", false);
                sickLitAnim.SetBool("girlIsHere", false);
                particleEmit = false;
                clicSound.SetActive(false);
                soundPlayed = false;
                fireSound.volume = fireVolumeStart;
            }
        }
    }
}
