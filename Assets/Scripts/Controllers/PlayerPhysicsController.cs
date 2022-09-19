using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour, IGeterGameObject
    {
        [SerializeField]
        PlayerManager playerManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject),out Component getterGameObject))
            {
                playerManager.IsEnterTurret(getterGameObject.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
           

        }

    }
}