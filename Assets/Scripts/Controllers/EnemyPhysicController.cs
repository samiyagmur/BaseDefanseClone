using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using AIBrain;

namespace Contollers
{
    public class EnemyPhysicController : MonoBehaviour
    {
        private Transform _detectedPlayer;
        private Transform _detectedMine;
        //private EnemyBrain _enemyAIBrain;
        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        //private void Awake()
        //{
        //    _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyBrain>();
        //}
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player is Found!");
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;

                //sinyalle çakmayý dene
                // _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
            }

            ///if (other.GetComponent<Mine>())
            //{
            //    _detectedMine = other.GetComponent<Mine>();
            //}/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                //this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
            }

            ///if (other.GetComponent<Mine>())
            //{

            //}/
        }
    } 
}