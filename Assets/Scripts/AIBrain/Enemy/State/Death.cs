using Abstraction;
using AIBrain;
using DG.Tweening;
using Enums;
using Interfaces;
using Managers;
using Signals;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace State
{
    public class Death : IState, IReleasePoolObject,IGetPoolObject
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private EnemyType _enemyType;

        public Death(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, EnemyType enemyType)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _enemyType = enemyType;
        }
        private void EnemyDoDead(PoolType type)
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                _enemyBrain.transform.DOMoveY(-3f, 1f).OnComplete(() => ReleaseObject(_enemyBrain.gameObject, type));
            });
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        public  void Enter()
        {

            var poolType = (PoolType)Enum.Parse(typeof(PoolType), _enemyType.ToString());
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Die");
            EnemyDoDead(poolType);
            for (int i = 0; i < 3; i++)
            {
                
                var creatableObj = GetObject(PoolType.Money);
                Debug.Log(creatableObj.name);
                creatableObj.transform.position = _enemyBrain.transform.position+new Vector3(0,5,0);   
            }

        }

        public void Exit()
        {
          
        }

  
        public void Tick()
        {
          
        }
        



    }
}