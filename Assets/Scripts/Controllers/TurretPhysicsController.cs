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
            

            if (other.TryGetComponent(typeof(PlayerManager), out Component getterGameObject))
            {
                _turretManager.IsEnterUser();
            }

            //if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            //{
    
            //    _turretManager.IsEnemyEnterTurretRange(other.gameObject);

            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(PlayerManager), out Component getterGameObject))
            {
                _turretManager.IsExitUser();
            }
            //if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            //{
            //    _turretManager.IsEnemyExitTurretRange();
            //}
        }

        private void OnTriggerStay(Collider other)
        {
            //if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            //{
            //    _turretManager.IsFollowEnemyInTurretRange();
            //}
        }



    }
}