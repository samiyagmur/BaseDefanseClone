using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class CreatWorkerPhysics : MonoBehaviour
    {
        [SerializeField]
        private CreatWorkerManager creatWorkerManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(PlayerManager), out Component playerObject))
            {
                creatWorkerManager.IsPlayerHit();
            }
        }

    }
}