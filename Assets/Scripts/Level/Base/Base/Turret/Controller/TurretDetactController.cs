using AIBrain;
using AIBrains.EnemyBrain;
using Assinger;
using Controllers.AIControllers;
using Enums;
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
        private TurretKey turretKey;

        public TurretKey TurretKey { get => turretKey; }

        //private void OnEnable()
        //{

        //    _turretManager.AddNewTurret(turretKey);

        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
               
                _turretManager.IsEnemyEnterTurretRange(other.transform.parent.gameObject, TurretKey);

                //_turretManager.OtoRotate(TurretKey);

                //_turretManager.SpinGattaling(TurretKey);

                //_turretManager.Attack(TurretKey);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController  enemy))
            {
                _turretManager.IsEnemyExitTurretRange(TurretKey);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
                _turretManager.OtoRotate(TurretKey);

                _turretManager.SpinGattaling(TurretKey);

                _turretManager.Attack(TurretKey);
            }
        }

    }
}