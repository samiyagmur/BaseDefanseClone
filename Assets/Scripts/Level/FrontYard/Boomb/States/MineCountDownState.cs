using AIBrain.MineBrain;
using Interfaces;
using UnityEngine;

namespace AI.States
{
    public class MineCountDownState : IState
    {
        #region Self Variables

        #region Public Variables

        public bool IsTimerDone => (timer >= _mineBrain.mineManager.MineCountDownTime);

        #endregion Public Variables

        #region Private Variables

        private float timer = 0;
        private MineBrain _mineBrain;

        #endregion Private Variables

        #endregion Self Variables

        public MineCountDownState(MineBrain mineBrain)
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
        }

        private void ResetTimer()
        {
            timer = 0;
        }

        public void OnExit()
        {
            ResetTimer();
        }
    }
}