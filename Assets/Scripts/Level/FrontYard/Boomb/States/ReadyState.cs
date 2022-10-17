using Interfaces;
using UnityEngine;

namespace AI.States
{
    public class ReadyState : IState
    {
        public void OnEnter()
        {
            Debug.Log("readyState");
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
        }
    }
}