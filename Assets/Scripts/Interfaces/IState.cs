using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Abstraction
{
    public interface IState
    {
        void  OnEnter();
        void Tick();
        void  OnExit();
        
    }
}