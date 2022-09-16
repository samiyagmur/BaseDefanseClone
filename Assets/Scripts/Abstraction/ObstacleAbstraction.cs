using Enums;
using System.Collections;
using UnityEngine;

namespace Abstraction
{
    public abstract class ObstacleAbstraction : MonoBehaviour
    {
        public virtual void ChangeGateState(GateState state) { }

        public virtual void MoveRoate(float rotate) { }
    }
}