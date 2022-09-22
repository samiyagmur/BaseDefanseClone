using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public  class AmmoWorkerPhysicController : MonoBehaviour
    {   


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoShopManager),out Component ammoShopManager))
            {
                
            }
        }


    }
}