using Abstraction;
using Managers;
using StateMachines;
using UnityEngine;

namespace Boomb
{
    public class LureState : IState
    {
        private MineBrain _mineBrain;
        private float timer = 0;
        public bool IsTimerDone => timer >= _mineBrain.mineManager.LureTime;
        public LureState(MineBrain mineBrain)
        {
            _mineBrain = mineBrain;
        }
        public void Tick()
        {
            timer += Time.deltaTime;
        }

        public void Enter()
        {
            Debug.Log("LureState");
            ResetTimer();
            _mineBrain.mineManager.LureColliderState(true);
        }

        public void Exit()
        {
            ResetTimer();
            _mineBrain.mineManager.LureColliderState(false);
        }

        private void ResetTimer()
        {
            timer = 0;
        }
    }
}