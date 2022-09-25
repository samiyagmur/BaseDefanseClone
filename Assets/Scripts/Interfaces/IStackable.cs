using Enums;
using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IStackable
    {
        void AddStack(Vector3 stackPositon, GameObject stackObject, Transform startPoint);

        void RemoveStack();

        void StartStack(StackStatus stackStatus);

        void StopStack(StackStatus stackStatus);


    }
}