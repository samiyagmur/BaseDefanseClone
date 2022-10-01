using AIBrain;
using Managers;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {

        private AmmoManager _ammoManager;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {
           

            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();

                _ammoManager.IsAmmoEnterAmmoWareHouse();
            }
            if (other.TryGetComponent(typeof(AmmoContaynerPhysicsControl), out Component ammoContayenr))//it must change
            {
               
               _ammoManager.IsEnterTurretContayner();
            }
        }


        private void OnTriggerExit(Collider other)
        {
           
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();
                _ammoManager.IsAmmoExitAmmoWareHouse();

                _ammoManager.ResetItems();
            }

            if (other.TryGetComponent(typeof(AmmoContaynerPhysicsControl), out Component ammoContayenr))//it must change
            {
                _ammoManager.IsExitTurretContayner();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                _ammoManager = other.gameObject.GetComponent<AmmoManager>();

                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.8f;

                    _ammoManager.IsAmmoWorkerStayOnAmmoWareHouse();
    
                }
            }
           
        }



    }
}