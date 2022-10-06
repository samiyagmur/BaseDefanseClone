using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class TurretDetactController : MonoBehaviour
    {
        [SerializeField]
        private TurretManager _turretManager;
        private float _timer=0.3f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            {
                _turretManager.IsEnemyEnterTurretRange(other.gameObject, transform.parent.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            {
                _turretManager.IsEnemyExitTurretRange(transform.parent.gameObject);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(typeof(EnemyBrain), out Component enemy))
            {
                _timer -= Time.deltaTime;

                if (_timer < 0)
                {
                    _timer = 2f;

                    _turretManager.IsFollowEnemyInTurretRange(transform.parent.gameObject);

                    _turretManager.IsAttackToEnemy();
                }

            }
        }

    }
}