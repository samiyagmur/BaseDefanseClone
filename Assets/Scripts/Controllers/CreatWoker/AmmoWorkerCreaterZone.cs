using Managers;
using Signals;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoWorkerCreaterZone : MonoBehaviour
    {
        [SerializeField]
       
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent( out PlayerPhysicsController playerPhysicsController))
            {
                AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
            }


        }


    }
}