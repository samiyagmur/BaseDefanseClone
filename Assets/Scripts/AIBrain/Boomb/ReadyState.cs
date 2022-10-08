using Abstraction;
using System.Collections;
using UnityEngine;

namespace Boomb
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