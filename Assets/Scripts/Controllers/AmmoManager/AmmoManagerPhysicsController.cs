using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoManagerPhysicsController : MonoBehaviour
    {
        [SerializeField]
        AmmoWorkerBaseManager ammoWorkerBaseManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AmmoWorkerPhysicsController ammoWorkerPhysicsController))
            {
                ammoWorkerBaseManager.SendAmmoToWorker(other.gameObject.transform);
            }



        }

    }
}