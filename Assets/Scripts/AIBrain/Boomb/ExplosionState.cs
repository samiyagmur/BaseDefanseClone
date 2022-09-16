using System.Threading;
using System.Threading.Tasks;
using Abstraction;
using Managers;
using StateMachines;
using UnityEngine;

namespace Boomb
{
    public class ExplosionState : IState
    {
        private MineBrain _mineBrain;
        private float _timer;

        private bool isExplosionHappened = false;
        public bool IsExplosionHappened => isExplosionHappened;
        public ExplosionState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {
        }

        public void Enter()
        {
            Debug.Log("ExplosionState");
            _mineBrain.mineManager.ExplosionColliderState(true);
            isExplosionHappened = true;
        }

        public async void Exit()
        {
            await Task.Delay(200);
            Debug.Log("Onexit explosion");
            _mineBrain.mineManager.ExplosionColliderState(false);
            isExplosionHappened = false;
        }

    }
}