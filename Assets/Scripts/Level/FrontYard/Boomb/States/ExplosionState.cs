using AIBrain.MineBrain;
using Interfaces;
using UnityEngine;

namespace AI.States
{
    public class ExplosionState : IState
    {
        private MineBrain _mineBrain;
        private float _timer;

        private bool isExplosionHappened;
        public bool IsExplosionHappened => _timer >= 0.3f;

        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }

        public void Tick()
        {
            Debug.Log("explosionstate");
            _timer += Time.deltaTime;
        }

        public void OnEnter()
        {
            _mineBrain.mineManager.ExplosionColliderState(true);
            //isExplosionHappened=true;
            ResetTimer();
        }

        public void OnExit()
        {
            _mineBrain.mineManager.ExplosionColliderState(false);
            isExplosionHappened = false;
            ResetTimer();
        }

        private void ResetTimer()
        {
            _timer = 0;
        }
    }
}

//using System.Threading;
//using System.Threading.Tasks;
//using Abstraction;
//using Managers;
//using StateMachines;
//using UnityEngine;

//namespace Boomb
//{
//    public class ExplosionState : IState
//    {
//        private MineBrain _mineBrain;
//        private float _timer;

//        private bool isExplosionHappened = false;
//        public bool IsExplosionHappened => isExplosionHappened;
//        public ExplosionState(MineBrain mineBrain)
//        {
//            _mineBrain = mineBrain;
//        }
//        public void Tick()
//        {
//        }

//        public void Enter()
//        {
//            Debug.Log("ExplosionState");
//            _mineBrain.mineManager.ExplosionColliderState(true);
//            isExplosionHappened = true;
//        }

//        public void Exit()
//        {
//            isExplosionHappened = false;

//            Debug.Log("Onexit explosion");
//            _mineBrain.mineManager.ExplosionColliderState(false);

//        }

//    }
//}