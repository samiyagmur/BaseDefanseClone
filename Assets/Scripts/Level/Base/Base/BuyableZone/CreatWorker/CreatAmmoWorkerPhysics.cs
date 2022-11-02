using Enums;
using Interfaces;
using Signals;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class CreatAmmoWorkerPhysics : MonoBehaviour
    {
        [SerializeField]
        private int price;

        [SerializeField]
        private Transform paymentTarget;

        [SerializeField]
        private TextMeshPro priceText;

        private bool _canTake;
        private const int _payedAmount = 10;

        private void Awake()
        {
            priceText.text = price.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            StartPayment(customer.CanPay, customer);
        }

        public void StartPayment(bool canTake, ICustomer customer)
        {
            _canTake = canTake;
            if (!_canTake)
                return;
            UpdatePayment(customer);
        }

        private async void UpdatePayment(ICustomer customer)
        {
            if (price == 0)
            {
                AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea?.Invoke(transform);
                transform.parent.gameObject.SetActive(false);

                _canTake = false;
                customer.CanPay = false;
            }

            if (!_canTake || !customer.CanPay)
            {
                _canTake = true;
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                return;
            }
            customer.PaymentAnimation(paymentTarget, PoolType.Money);
            price -= _payedAmount;
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
            CoreGameSignals.Instance.onUpdateMoneyScore(-_payedAmount);
            priceText.text = price.ToString();

            await Task.Delay(100);

            UpdatePayment(customer);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.CanPay = false;
            StopPayment(customer.CanPay);
        }

        public void StopPayment(bool canTake) => _canTake = canTake;
    }
}