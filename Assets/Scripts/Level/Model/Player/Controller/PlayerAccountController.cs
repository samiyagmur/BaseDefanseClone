using Controller;
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
            if (other.TryGetComponent(out ObstaclePhysicsController gatePhysics))
            {
                playerMoneyStackerController.OnRemoveAllStack();
            }
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