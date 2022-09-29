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
            }
            if (other.TryGetComponent(typeof(AmmoContaynerPhysicsControl), out Component ammoContayenr))//it must change
            {
                ammoManager.IsExitTurretContayner();
            }
        }



    }
}