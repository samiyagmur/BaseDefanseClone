﻿using Abstraction;
using Concreate;
using Interfaces;
using Signals;
using System.Threading.Tasks;
using UnityEngine;

namespace Controllers
{
    public class PlayerAccountController : MonoBehaviour, ICustomer
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private MoneyStackerController moneyStackerController;

        #endregion

        #region Private Variables

        private bool _canPay = true;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<StackableMoney>(out StackableMoney stackable))
            {
                CollectMoney(stackable);
                CoreGameSignals.Instance.onUpdateMoneyScore.Invoke(+1);
            }
            if (other.TryGetComponent<StackableGem>(out StackableGem stackableGem))
            {

            }
            else if (other.TryGetComponent<Interactable>(out Interactable interactable))
            {
                moneyStackerController.OnRemoveAllStack();
            }
        }
        private void CollectMoney(IStackable stackable)
        {
            moneyStackerController.SetStackHolder(stackable.SendToStack().transform);
            moneyStackerController.GetStack(stackable.SendToStack());
        }

        #region Paying Interaction
        public bool HasMoney { get => CoreGameSignals.Instance.onHasEnoughMoney.Invoke(); set { } }
        public async void MakePayment()
        {
            while (true)
            {
                CoreGameSignals.Instance.onUpdateMoneyScore.Invoke(-1);

                if (HasMoney && _canPay)
                {
                    await Task.Delay(100);

                    continue;
                }
                break;
            }
        }
        public async void StopPayment()
        {
            _canPay = false;
            await Task.Delay(200);
            _canPay = true;
        }
        #endregion
    }
}