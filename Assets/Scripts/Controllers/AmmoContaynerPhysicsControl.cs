using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class AmmoContaynerPhysicsControl : MonoBehaviour
    {
        private float timer;

        private float timeScale;
        [SerializeField]
        private AmmoContaynerManager _ammoContaynerManager;
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(AmmoWorkerManager), out Component ammoWorkerObject))
            {   
                if (timer < 0.1f)
                {
                    timer = Time.deltaTime;
                }
                else
                {
                    _ammoContaynerManager.IsHitAmmoWorker(); //ListesiGelcek

                    timer = 0;
                }
            }
        }
    }
}