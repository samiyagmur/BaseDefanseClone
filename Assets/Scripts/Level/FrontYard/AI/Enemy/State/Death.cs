using DG.Tweening;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain
{
    public class Death : IState, IGetPoolObject, IReleasePoolObject
    {
        #region Self Variables



        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private EnemyAIBrain _enemyAIBrain;
        private EnemyType _enemyType;
        private static readonly int Die = Animator.StringToHash("Die");

        #endregion Private Variables

        #endregion Self Variables

        public Death(NavMeshAgent navMeshAgent, Animator animator, EnemyAIBrain enemyAIBrain, EnemyType enemyType)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _enemyType = enemyType;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            EnemyDead();
            _navMeshAgent.enabled = false;
            _animator.SetTrigger(Die);
            for (int i = 0; i < 3; i++)
            {
                var createObj = GetObject(PoolType.Money);

                if (createObj == null) return;
                if (_enemyAIBrain.transform.position == null) return;
                createObj.transform.position = _enemyAIBrain.transform.position + new Vector3(0, 3, 0);
            }
        }

        public void OnExit()
        {
        }

        private void EnemyDead()
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), _enemyType.ToString());

            DOVirtual.DelayedCall(1f, () =>
            {
                _enemyAIBrain.transform.DOMoveY(-3f, 1f).OnComplete(() => ReleaseObject(_enemyAIBrain.gameObject, poolType));
            });
            AISignals.Instance.onReleaseObjectUpdate?.Invoke(_enemyAIBrain.gameObject);
        }

        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
    }
}