﻿using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using Interfaces;

namespace Controllers
{
    public class TurretStackPhysicsControl : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                
            }
        }
  
    }
}