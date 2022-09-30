using AIBrain;
using Managers;
using System.Collections;
using System.Threading.Tasks;
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
  
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                ammoManager.IsAmmoEnterAmmoWareHouse();
            }
            if (other.TryGetComponent(typeof(AmmoContaynerPhysicsControl), out Component ammoContayenr))//it must change
            {
                ammoManager.IsEnterTurretContayner();
            }

        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                ammoManager.IsAmmoExitAmmoWareHouse();

                ammoManager.ResetItems();
            }
            if (other.TryGetComponent(typeof(AmmoContaynerPhysicsControl), out Component ammoContayenr))//it must change
            {
                ammoManager.IsExitTurretContayner();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 0.8f;

                   
                    ammoManager.IsAmmoWorkerStayOnAmmoWareHouse();
    
                }
            }
           
        }



    }
}