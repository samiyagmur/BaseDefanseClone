using Enums;
using Interfaces;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerDetectionController : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if (manager.CurrentAreaType == AreaType.BaseDefense) return;

            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.IsTaken) return;

                manager.EnemyList.Add(damagable);

                damagable.IsTaken = true;

                if (manager.EnemyTarget == null)
                {
                    manager.SetEnemyTarget();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.IsTaken = false;

                manager.EnemyList.Remove(damagable);

                manager.EnemyList.TrimExcess();

                if (manager.EnemyList.Count == 0)
                {
                    manager.EnemyTarget = null;

                    manager.HasEnemyTarget = false;
                }
                if (damagable.IsDead)
                {
                    manager.HasEnemyTarget = false;
                }
            }
        }
    }
}