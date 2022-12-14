using AIBrains.SoldierBrain;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class Idle : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private Transform _tentPosition;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        public Idle(SoldierAIBrain soldierAIBrain, Transform tentPosition, NavMeshAgent navMeshAgent, Animator animator)
        {
            _soldierAIBrain = soldierAIBrain;
            _tentPosition = tentPosition;
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public void Tick()
        {
        }

        private void GetTentSpawnPosition()
        {
            bool TentSpawnPosition(Vector3 center, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 point = center;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(point, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }
            if (!TentSpawnPosition(_tentPosition.position, out var point)) return;
            _navMeshAgent.Warp(point);
        }

        public void OnEnter()
        {
            GetTentSpawnPosition();
            _navMeshAgent.enabled = true;
        }

        public void OnExit()
        {
        }
    }
}