using AIBrain;
using AIBrains.EnemyBrain;
using Assinger;
using Controllers.AIControllers;
using Enums;
using Interfaces;
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
        private TurretID turretID;

        public TurretID TurretID { get => turretID;  }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
                _turretManager.IsEnemyEnterTurretRange(other.transform.parent.gameObject, TurretID.GetId);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
                _turretManager.OtoRotate(TurretID.GetId);

                _turretManager.SpinGattaling(TurretID.GetId);

                _turretManager.Attack(TurretID.GetId);
            }
        }

    }
}