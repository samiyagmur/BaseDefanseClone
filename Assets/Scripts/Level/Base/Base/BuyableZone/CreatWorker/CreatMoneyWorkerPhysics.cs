using Signals;
using UnityEngine;

namespace Controllers
{
    public class CreatMoneyWorkerPhysics : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                MoneyWorkerSignals.Instance.onGenerateMoneyWorker?.Invoke(transform);
            }
        }
    }
}