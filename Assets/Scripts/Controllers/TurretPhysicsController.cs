using Interfaces;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretPhysicsController : MonoBehaviour,IGeterGameObject
    {
        
        [SerializeField]
        private TurretManager _turretManager;

      
        private void OnTriggerEnter(Collider other)
        {
            

            if (other.TryGetComponent(typeof(IGeterGameObject), out Component getterGameObject))
            {
                _turretManager.IsEnterUser();
            }

            if (other.TryGetComponent(typeof(EnemyManager), out Component enemy))
            {
    
                _turretManager.IsEnemyEnterTurretRange(other.gameObject);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject), out Component getterGameObject))
            {
                _turretManager.IsExitUser();
            }
            if (other.TryGetComponent(typeof(EnemyManager), out Component enemy))
            {
                _turretManager.IsEnemyExitTurretRange();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyManager), out Component enemy))
            {
                _turretManager.IsFollowEnemyInTurretRange();
            }
        }



    }
}