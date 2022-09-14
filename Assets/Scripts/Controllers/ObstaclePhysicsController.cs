﻿using Managers;
using System.Collections;
using UnityEngine;


namespace Controllers
{
    public class ObstaclePhysicsController : MonoBehaviour
    {

        [SerializeField]
        ObstacleManager obstacleManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) obstacleManager.IsHitEnterPlayer();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) obstacleManager.IsHitExitPlayer();
        }
    }
}