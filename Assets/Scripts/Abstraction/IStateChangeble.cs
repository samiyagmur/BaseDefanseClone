using Enums;
using System.Collections;
using UnityEngine;

namespace Abstraction
{
    interface IStateChangeble
    {
        void ChangeGateState(GateState state);
    }

}