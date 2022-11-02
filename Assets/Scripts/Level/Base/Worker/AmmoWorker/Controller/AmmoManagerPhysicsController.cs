using AIBrain.AmmoWorkers;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class AmmoManagerPhysicsController : MonoBehaviour
    {
        [SerializeField]
        private AmmoManager ammoManager;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))
            {
                ammoManager.WhenAmmoworkerEnterAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>());

                ammoManager.WhenGetTurretStackInfo(other.transform.parent.GetComponent<AmmoWorkerBrain>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))
            {
                ammoManager.WhenAmmoworkerExitAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))//it must change
            {
                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.1f;

                    ammoManager.WhenStayOnAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>(),
                                                        other.transform.parent.GetComponent<AmmoWorkerStackController>());
                }
            }
        }
    }
}