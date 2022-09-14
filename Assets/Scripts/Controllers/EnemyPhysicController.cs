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

        private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) brain.PlayerTarget = other.transform; }
        private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) brain.PlayerTarget = null; }
    } 
}