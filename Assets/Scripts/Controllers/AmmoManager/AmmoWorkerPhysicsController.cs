using AIBrain;
using Managers;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerPhysicsController : MonoBehaviour
    {
        
        private AmmoWorkerBaseManager _ammoManager;

        [SerializeField]
        private AmmoStackerController _stackerController;

        private AmmoWorkerBrain _brain;

        private float _timer = 0.4f;

        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                 _brain.IsAmmoWorkerReachedWareHouse = true;
            }
           
            if (other.TryGetComponent(out AmmoDropZonePhysicsController ammoDropZonePhysicsController))//it must change
            {

            }
        }


        private void OnTriggerExit(Collider other)
        {
           
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {


                _brain.IsAmmoWorkerReachedWareHouse = false;

            }
            if (other.TryGetComponent(typeof(AmmoDropZonePhysicsController), out Component ammoContayenr))//it must change
            {   

            }
        }

        private void OnTriggerStay(Collider other)
        {
            
            if (other.TryGetComponent(typeof(AmmoManagerPhysicsController), out Component ammoManagment))//it must change
            {
                
            }

        }



    }
}