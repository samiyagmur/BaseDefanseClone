using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IStackable
    {
         void AddStack(List<Vector3> stackPositon, Transform startPoint, int maxAmountForStack);

        void RemoveStack();

        void StartStack(StackStatus stackStatus);

        void StopStack(StackStatus stackStatus);


    }
}