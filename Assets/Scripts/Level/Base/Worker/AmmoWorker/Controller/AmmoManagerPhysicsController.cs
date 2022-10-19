using AIBrain.AmmoWorkers;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoManagerPhysicsController : MonoBehaviour
    {
        [SerializeField]
        private AmmoManager _ammoManager;

        private void OnTriggerEnter(Collider other)
        {
            _ammoManager.WhenAmmoworkerEnterAmmoWareHouse(other.GetComponent<AmmoWorkerBrain>());

            _ammoManager.WhenGetTurretStackInfo(other.GetComponent<AmmoWorkerBrain>());
        }
        private void OnTriggerExit(Collider other)
        {
            _ammoManager.WhenAmmoworkerExitAmmoWareHouse(other.GetComponent<AmmoWorkerBrain>());

            //_ammoManager.WhenAmmoworkerExitAmmoWareHouse(other.GetComponent<AmmoWorkerStackController>());
        }

    }
}