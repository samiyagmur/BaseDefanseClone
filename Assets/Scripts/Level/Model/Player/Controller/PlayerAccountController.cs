using Concreate;
using Controller;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerAccountController : MonoBehaviour, ICustomer
    {
        public SphereCollider Collider;

        [SerializeField] private PlayerStackerController playerMoneyStackerController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableMoney>(out StackableMoney stackableMoney))
            {
                stackableMoney.IsCollected = true;
                MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                playerMoneyStackerController.SetStackHolder(stackableMoney.SendToStack().transform);
                playerMoneyStackerController.GetStack(stackableMoney.SendToStack());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out GatePhysicsController gatePhysics))
            {
                playerMoneyStackerController.OnRemoveAllStack();
            }
        }

        #region Account

        public bool CanPay
        { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }

        public void PaymentAnimation(Transform stackableTransform, PoolType stackabletype)
        {
            playerMoneyStackerController.PaymentStackAnimation(stackableTransform, stackabletype);
        }

        #endregion Account
    }
}