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
                Debug.Log(other.gameObject.name);
              ammoManager.IsAmmoEnterAmmoWareHouse();
            }
            
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicsController), out Component ammoManagment))//it must change
            {
                ammoManager.IsAmmoExitAmmoWareHouse();
            }
        }



    }
}