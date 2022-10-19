using AIBrain.AmmoWorkers;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {

        private AmmoManager _ammoManager;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {   

            if (other.TryGetComponent(out AmmoManagerPhysicsController ammoManagment))//it must change
            {
                _ammoManager = other.transform.parent.gameObject.GetComponent<AmmoManager>();

                //_ammoManager.GiveTargetInAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerBrain>());
            }
           
            if (other.TryGetComponent(out TurretStackController turretStackController))//it must change
            {
                _ammoManager.WhenEnterTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.WhenEnterTurretStack(turretStackController,transform.parent.gameObject.GetComponent<AmmoWorkerStackController>());
            }
        }


        private void OnTriggerExit(Collider other)
        {
           
            if (other.TryGetComponent(out AmmoManagerPhysicsController ammoManagment))//it must change
            {
                _ammoManager = other.transform.parent.GetComponent<AmmoManager>();

                

                _ammoManager.ResetItems();
            }
            if (other.TryGetComponent(out TurretStackPhysicsControl ammoContayenr))//it must change
            {   
                _ammoManager.WhenExitTurretStack(transform.parent.GetComponent<AmmoWorkerBrain>());

                _ammoManager.WhenExitOnTurretStack(transform.parent.GetComponent<AmmoWorkerStackController>());

               // _ammoManager.IsAmmoWorkerStackEmpty(transform.parent.GetComponent<AmmoWorkerBrain>());
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.transform.parent.gameObject.GetComponent<AmmoManager>();

                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.1f;
           
                    _ammoManager.WhenStayOnAmmoWareHouse(transform.parent.GetComponent<AmmoWorkerBrain>(),
                                                        transform.parent.GetComponent<AmmoWorkerStackController>());
                }
            }

        }



    }
}