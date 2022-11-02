using AIBrains.SoldierBrain;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class SoldierDetectionController : MonoBehaviour
    {

        [SerializeField]
        private SoldierAIBrain soldierAIBrain;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.IsTaken) return;
                damagable.IsTaken = true;
                soldierAIBrain.EnemyList.Add(damagable);
                if (soldierAIBrain.EnemyTarget == null)
                {
                    soldierAIBrain.EnemyTarget = soldierAIBrain.EnemyList[0].GetTransform();
                    soldierAIBrain.DamageableEnemy = soldierAIBrain.EnemyList[0];
                    soldierAIBrain.HasEnemyTarget = true;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (soldierAIBrain.EnemyList.Count == 0) return;
                soldierAIBrain.EnemyList.Remove(damagable);
                soldierAIBrain.EnemyList.TrimExcess();
                if (soldierAIBrain.EnemyList.Count == 0)
                {
                    soldierAIBrain.EnemyTarget = null;
                    soldierAIBrain.HasEnemyTarget = false;
                }
                damagable.IsTaken = false;
            }
        }
    }
}