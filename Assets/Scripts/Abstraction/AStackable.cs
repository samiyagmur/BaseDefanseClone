using Concreate;
using DG.Tweening;
using Interfaces;
using Signals;
using UnityEngine;

namespace Abstraction
{
    public abstract class AStackable : MonoBehaviour, IStackable
    {
        public virtual bool IsSelected { get; set; }
        public virtual bool IsCollected { get; set; }

        public virtual void SetInit(Transform initTransform, Vector3 position)
        {
        }

        public virtual void SetVibration(bool isVibrate)
        {
        }

        public virtual void SetSound()
        {
        }

        public virtual void EmitParticle()
        {
        }

        public virtual void PlayAnimation()
        {
        }

        public abstract GameObject SendToStack();

        public virtual void SendStackable(StackableMoney stackableMoney)
        {
            DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onSetStackable?.Invoke(stackableMoney));
        }

        public virtual void SendStackableOndisable(StackableMoney stackableMoney)
        {
            DOVirtual.DelayedCall(0.1f, () => MoneyWorkerSignals.Instance.onRemoveStackable?.Invoke(stackableMoney));
        }
    }
}