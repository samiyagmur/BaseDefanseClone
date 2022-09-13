using Abstraction;
using Assets.Scripts.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.Enemy.State
{
    public class Search :IState
    {
        protected  Animator _animator;

        protected  NavMeshAgent _navMeshAgent;

        protected  EnemyBrain _enemyBrain;

        private Transform _spawnPoint;

        private bool _HasTarget=false;

        public Search(Animator animator, NavMeshAgent navMeshAgent, EnemyBrain enemyBrain, Transform spawnPoint)
        {
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _enemyBrain = enemyBrain;
            _spawnPoint = spawnPoint;
        }

        public  void Enter()
        {
            _enemyBrain.enabled = true;
            GetRandomPointBakedSurface();
            
        }


        private void GetRandomPointBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere * range;
                    Vector3 randomPosition = new Vector3(randomPoint.x, 0, center.z);
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }
            Vector3 point;
            if (!RandomPoint(_spawnPoint.position, 20, out point)) return;
            _navMeshAgent.Warp(point);
        }


        public void Exit() { }

        public void Tick() { }

    }
}
