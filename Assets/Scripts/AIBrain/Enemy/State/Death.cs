using Abstraction;
using AIBrain;
using Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace State
{
    public class Death : IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        public Death(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
        }

        public  void Enter()
        {
            Debug.Log("dd");
            _enemyBrain.Buuumm();

           // ObjectPoolManager.Instance.ReturnObject(_enemyBrain.gameObject);
        }

        public void Exit()
        {
          
        }

        public void Tick()
        {
          
        }
    }
}