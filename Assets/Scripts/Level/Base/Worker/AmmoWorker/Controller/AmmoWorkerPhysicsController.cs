using AIBrain.AmmoWorkers;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {
        private AmmoManager _ammoManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AmmoManagerPhysicsController ammoManagerPhysicsController))
            {
                _ammoManager = other.transform.parent.GetComponent<AmmoManager>();
            }

            if (other.TryGetComponent(out AmmoDropZonePhysicsControl turretStackPhysicsControl))//it must change
            {
                _ammoManager.WhenEnterTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>(), turretStackPhysicsControl.TurretKey);

                _ammoManager.WhenEnterTurretStack(other.transform.parent.GetComponent<AmmoDropZoneController>(),
                    transform.parent.gameObject.GetComponent<AmmoWorkerStackController>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out AmmoDropZonePhysicsControl turretStackPhysicsControl))//it must change
            {
                _ammoManager.WhenExitTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.WhenExitOnTurretStack(transform.parent.GetComponent<AmmoWorkerStackController>());
            }
        }
    }
}