using Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Abstraction
{
    public abstract class AStacker : MonoBehaviour
    {
        public List<GameObject> StackList = new List<GameObject>();

        public virtual void SetStackHolder(Transform otherTransform)
        {
            otherTransform.SetParent(transform);
        }

        public virtual void GetStack(GameObject stackableObj)
        {
        }

        public virtual void GetStack(GameObject stackableObj, Transform otherTransform)
        {
        }

        public virtual void GetAllStack(IStackable stack)
        {
        }

        public virtual void RemoveStack(IStackable stackable)
        {
        }

        public virtual void ResetStack(IStackable stackable)
        {
        }
    }
}