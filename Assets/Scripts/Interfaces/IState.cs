using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Abstraction
{
    public interface IState
    {
        void  Enter();
        void Tick();
        void  Exit();
        
    }
}