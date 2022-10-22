using AIBrain;
using AIBrains.EnemyBrain;
using Assinger;
using Controllers.AIControllers;
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
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {


                _turretManager.IsSelectCurrentTurret(turretKeyAssinger.TurretKey);

                _turretManager.IsEnemyEnterTurretRange(other.transform.parent.gameObject);

             
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController  enemy))
            {
                _turretManager.IsEnemyExitTurretRange();
            }
        }

    }
}