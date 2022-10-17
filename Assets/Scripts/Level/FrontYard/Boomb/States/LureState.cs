using AIBrain.MineBrain;
using Interfaces;
using UnityEngine;

namespace AI.States
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

        public void OnEnter()
        {
            Debug.Log("LureState");
            ResetTimer();
            _mineBrain.mineManager.LureColliderState(true);
        }

        public void OnExit()
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