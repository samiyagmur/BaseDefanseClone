using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoManagerPhysicsController : MonoBehaviour
    {
        [SerializeField]
        private AmmoManager ammoManager;

       
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerPhysicController), out Component ammoWorkerManager))
            {
                ammoManager.IsAmmoWorkerHit();
                ammoManager.Invoke("IsNewList", 0.01f);
            }

        }


    }
}