using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using AIBrain;
using Abstraction;

namespace Contollers
{   
    public class EnemyPhysicController : MonoBehaviour
    {
        [SerializeField]
        private EnemyBrain brain;

        private Transform _detectPlayer;
        private bool _amIDead;
        private Transform _detectedMine;

        public bool IsBombInRange() => _detectedMine != null;

        public bool AmIDead { get => _amIDead; set => _amIDead = value; }

        private void OnTriggerEnter(Collider other) 
        { 
            
            if (other.CompareTag("Player")) brain.PlayerTarget = other.transform;

            if (other.CompareTag("LandMine"))
            {
                _detectedMine = other.transform;
                brain.MineTarget = _detectedMine;
                

            }
            if(other.CompareTag("Bullet"))
            {

                int damageAmount = other.GetComponent<IDamager>().GetDamage();

                //brain.Healt -= damageAmount;

                //if (brain.Healt<=0)
                //{
                //    _amIDead = true;
                //}

            }
            if (other.CompareTag("MineExplosion"))
            {
                Debug.Log("MineExplosion");
                int damageAmount = other.transform.parent.parent.GetComponent<IDamager>().GetDamage();
                Debug.Log(damageAmount);
               // brain.Healt -= damageAmount;
                //if (brain.Healt <= 0)
                //{
                //    _amIDead = true;
                    
                //}
            }
           
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Player")) brain.PlayerTarget = null;


            if (other.CompareTag("LandMine"))
            {
                _detectPlayer = null;
                brain.MineTarget = _detectedMine;
                brain.MineTarget = null;

            }

            if (other.CompareTag("MineExplosion"))
            {
                _detectPlayer = null;
                brain.MineTarget = _detectedMine;
                brain.MineTarget = null;
            }


        }

    
    } 
}