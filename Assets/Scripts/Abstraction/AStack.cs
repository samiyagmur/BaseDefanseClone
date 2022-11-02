﻿using Interfaces;
using UnityEngine;

namespace Abstraction
{
    public abstract class AStack : MonoBehaviour, IStack
    {
        public virtual void SetStackHolder(GameObject gameObject)
        {
        }

        public virtual void SetGrid()
        {
        }

        public virtual void SendGridDataToStacker()
        {
        }
    }
}