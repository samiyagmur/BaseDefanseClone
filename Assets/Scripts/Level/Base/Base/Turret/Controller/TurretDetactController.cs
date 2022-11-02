using Assinger;
using Controllers.AIControllers;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class TurretDetactController : MonoBehaviour
    {
        [SerializeField]
        private TurretManager turretManager;

        [SerializeField]
        private TurretID turretID;

        public TurretID TurretID { get => turretID; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
                turretManager.IsEnemyEnterTurretRange(other.transform.parent.gameObject, TurretID.GetId);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EnemyPhysicsController enemy))
            {
                turretManager.OtoRotate(TurretID.GetId);

                turretManager.SpinGattaling(TurretID.GetId);

                turretManager.Attack(TurretID.GetId);
            }
        }
    }
}