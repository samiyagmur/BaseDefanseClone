using System.Collections;
using System.Collections.Generic;
using Abstraction;
using AIBrain;

using UnityEngine;
using UnityEngine.AI;

public class BoombManager : IState
{
    private readonly EnemyBrain _enemyAIBrain;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly float _attackRange;
    private readonly float _chaseSpeed;
    private bool _attackOnPlayer;

    public BoombManager(NavMeshAgent navmeshAgent, Animator animator, EnemyBrain enemyAIBrain, float attackRange, float chaseSpeed)
    {
        _animator = animator;
        _attackRange = attackRange;
        _chaseSpeed = chaseSpeed;
        _enemyAIBrain = enemyAIBrain;
        _navMeshAgent = navmeshAgent;
    }
    public void Tick()
    {
        //if (_enemyAIBrain.Mine != null)
        //{
        //    _navMeshAgent.destination = _enemyAIBrain.MineTarget.transform.position;
        //}
    }
    public void OnEnter()
    {
        //_animator.SetTrigger("Walk");
        //_attackOnPlayer = false;
        //_navMeshAgent.speed = _chaseSpeed;
        //_navMeshAgent.SetDestination(_enemyAIBrain.MineTarget.transform.position);
    }

    public void OnExit()
    {

    }

}