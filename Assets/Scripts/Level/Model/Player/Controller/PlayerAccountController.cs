using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.PlayerControllers
{
    public class PlayerAccountController : MonoBehaviour, ICustomer
    {
        public SphereCollider Collider;

       [SerializeField] private MoneyStackerController playerMoneyStackerController;
        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent<IStackable>(out IStackable stackable))
            {
                stackable.IsCollected = true;
                MoneyWorkerSignals.Instance.onThisMoneyTaken?.Invoke();
                playerMoneyStackerController.SetStackHolder(stackable.SendToStack().transform);
               playerMoneyStackerController.GetStack(stackable.SendToStack());
            }


        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.TryGetComponent<GatePhysicsController>(out GatePhysicsController gatePhysics))
            //{
            //    playerMoneyStackerController.OnRemoveAllStack();
            //}
        }

        #region Account

        public bool CanPay { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }
        public void PlayPaymentAnimation(Transform transform)
        {
            //playerMoneyStackerController.PaymentStackAnimation(transform);
        }

        #endregion

    }
}