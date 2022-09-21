using Controllers;
using Enums;
using StateBehavior;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Abstraction
{
    public abstract class StateUsers : MonoBehaviour
    {
        internal abstract void Awake();
        internal abstract void GetData();
        internal abstract void GetStatesReferences();
        internal abstract void TransitionofState();
        internal abstract void SetEnemyAIData();
        internal abstract void Update();
    }
}