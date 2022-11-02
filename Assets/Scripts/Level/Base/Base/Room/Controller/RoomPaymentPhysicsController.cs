using Interfaces;
using Managers;
using UnityEngine;

namespace Controller
{
    public class RoomPaymentPhysicsController : MonoBehaviour
    {
        [SerializeField] private RoomManager roomManager;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("RoomPaymentPhysicsController");
            if (!other.TryGetComponent(out ICustomer customer)) return;
            roomManager.StartRoomPayment(customer.CanPay, customer);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ICustomer customer)) return;
            customer.CanPay = false;
            roomManager.StopRoomPayment(false);
        }
    }
}