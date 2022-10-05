﻿using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using Interfaces;

namespace Controllers
{
    public class TurretStackPhysicsControl : MonoBehaviour
    {
        private float _timer= 0.4f;
        [SerializeField]
        private AmmoContaynerManager _ammoContaynerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoContaynerManager.SetTurretStack(other.gameObject.transform.
                    parent.GetComponent<AmmoWorkerStackController>().SendAmmoStack());

                _ammoContaynerManager.IsHitAmmoWorker();
            }
        }
  
    }
}