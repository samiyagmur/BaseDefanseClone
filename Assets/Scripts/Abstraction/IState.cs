using System.Collections;
using UnityEngine;

namespace Abstraction
{
    public interface IState
    {
        void Tick();

        void Enter();

        void Exit();
        
    }
}