using AIBrain;
using AIBrain.EnemyBrain;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretDetactController : MonoBehaviour
    {
        [SerializeField]
        private TurretManager _turretManager;
        private float _timer=0.5f;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            {
                _turretManager.IsEnemyEnterTurretRange(enemy.gameObject, transform.parent.gameObject);

                _turretManager.IsSelectCurrentTurret(transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            {
                _turretManager.IsEnemyExitTurretRange(transform.parent.gameObject);
            }
        }

    }
}