using Controller;
using Data.ValueObject;
using Enums;
using Interfaces;
using Signals;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField]
        private RoomTypes roomTypes;

        [SerializeField]
        private RoomPaymentTextController roomPaymentTextController;

        [SerializeField] private Transform paymentTarget;

        private RoomData _roomData;
        private int _payedAmount = 10;
        private bool _canTake;

        private void Start()
        {
            _roomData = GetRoomData();
            SetRoomCost(_roomData.Cost);
        }

        private RoomData GetRoomData() => BaseSignals.Instance.onSetRoomData.Invoke(roomTypes);

        private void SetRoomCost(int cost) => roomPaymentTextController.SetInitText(cost);

        public void StartRoomPayment(bool canTake, ICustomer customer)
        {
            _canTake = canTake;
            if (!_canTake)
                return;
            UpdatePayment(customer);
        }

        public void StopRoomPayment(bool canTake) => _canTake = canTake;

        private async void UpdatePayment(ICustomer customer)
        {
            if (_roomData.Cost == 0)
            {
                _canTake = false;
                customer.CanPay = false;
                _roomData.SideBaseAvaliableStatus = SideBaseAvaliableStatus.Unlocked;
                BaseSignals.Instance.onChangeExtentionVisibility(roomTypes);
                UpdateRoomData();
            }

            if (!_canTake || !customer.CanPay)
            {
                _canTake = true;
                CoreGameSignals.Instance.onStopMoneyPayment?.Invoke();
                return;
            }
            customer.PaymentAnimation(paymentTarget, PoolType.Money);
            _roomData.Cost -= _payedAmount;
            CoreGameSignals.Instance.onStartMoneyPayment?.Invoke();
            CoreGameSignals.Instance.onUpdateMoneyScore(-_payedAmount);
            roomPaymentTextController.UpdateText(_roomData.Cost);
            UpdateRoomData();
            await Task.Delay(100);
            UpdatePayment(customer);
        }

        private void UpdateRoomData() => BaseSignals.Instance.onUpdateRoomData(_roomData, roomTypes);
    }
}