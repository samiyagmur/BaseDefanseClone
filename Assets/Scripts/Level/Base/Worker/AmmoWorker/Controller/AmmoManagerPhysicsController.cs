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
        private float _timer=0.4f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))
            {

                _ammoManager.WhenAmmoworkerEnterAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.WhenGetTurretStackInfo(other.transform.parent.GetComponent<AmmoWorkerBrain>());
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))
            {
                _ammoManager.WhenAmmoworkerExitAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>());
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
           
                    _ammoManager.WhenStayOnAmmoWareHouse(other.transform.parent.GetComponent<AmmoWorkerBrain>(),
                                                        other.transform.parent.GetComponent<AmmoWorkerStackController>());
                }
            }

        }

    }
}