using AIBrain.EnemyBrain;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class Atack :IState
    {
        private Animator _animator;

        private NavMeshAgent _navMeshAgent;

        private EnemyBrain _enemyBrain;

        private float _atackRange;

        public bool _inAttack;

        public Atack(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, float atackRange)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _atackRange = atackRange;
        }

        public void Tick()
        {
            if (_enemyBrain.PlayerTarget)
            {
                _navMeshAgent.destination = _enemyBrain.PlayerTarget.position;
            }
            else
            {
                Debug.Log("_inatack");
                _inAttack = false;
            }

            CheckAttackDistance();
        }

        public void OnEnter()
        {
            _navMeshAgent.SetDestination(_enemyBrain.PlayerTarget.position);
            _inAttack = true;
            _animator.SetTrigger("Attack");
        }

        private void CheckAttackDistance()
        {
            if (_navMeshAgent.remainingDistance > _atackRange)
            {
                _inAttack = false;
            }
        }

        public bool InPlayerAttackRange() => _inAttack;

        public void OnExit()
        {
        }
    }
}