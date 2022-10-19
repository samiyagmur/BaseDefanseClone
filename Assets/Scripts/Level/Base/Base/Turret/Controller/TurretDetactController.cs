using AIBrain;
using AIBrains.EnemyBrain;
using Assinger;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretDetactController : MonoBehaviour
    {
        [SerializeField]
        private TurretManager _turretManager;
        [SerializeField]
        private TurretKeyAssinger turretKeyAssinger;
        private float _timer=0.5f;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyAIBrain), out Component enemy))
            {
                
                _turretManager.IsEnemyEnterTurretRange(enemy.gameObject);

                _turretManager.IsSelectCurrentTurret(turretKeyAssinger.TurretKey);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyAIBrain), out Component enemy))
            {
                _turretManager.IsEnemyExitTurretRange();
            }
        }

    }
}