using System.Collections;
using UnityEngine;

namespace Abstraction
{
    public abstract class StateUsers : MonoBehaviour
    {


        internal virtual Transform CreatPoint { get ; set ; }
        internal virtual float Offset { get ; set; }
        internal virtual float StackHeigh { get; set; }
        internal virtual float Stackwidth { get; set; }
        internal virtual float MovementSpeed { get; set; }
        internal virtual Transform AmmoStore { get; set; }
        internal virtual Transform AmmoContainer { get; set; }

        internal abstract void Awake();
        internal abstract void SetAIData();

        internal abstract void GetStatesReferences();

        internal abstract void TransitionofState();

        internal abstract void Update();


    }

}