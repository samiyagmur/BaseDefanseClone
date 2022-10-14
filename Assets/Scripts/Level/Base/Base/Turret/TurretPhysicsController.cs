using AIBrain;
using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretPhysicsController : MonoBehaviour
    {
        
        [SerializeField]
        private TurretManager _turretManager;

        
      
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(PlayerPhysicsController), out Component getterGameObject))
            {
                _turretManager.IsEnterUser(transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(PlayerPhysicsController), out Component getterGameObject))
            {
                _turretManager.IsExitUser(transform.parent.gameObject);
            }
            
        }


    }
}