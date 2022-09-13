using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using AIBrain;

namespace Contollers
{
    public class EnemyPhysicController : MonoBehaviour
    {
        [SerializeField]
        private EnemyBrain brain;
        private Transform _detectedPlayer;
        


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer=other.transform;

                brain.SetPlayerTarget(_detectedPlayer);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //_detectedPlayer = null;

                this.gameObject.GetComponentInParent<EnemyBrain>().SetPlayerTarget(null);
               // brain.SetPlayerTarget(_detectedPlayer);

                
            }

        }

    } 
}