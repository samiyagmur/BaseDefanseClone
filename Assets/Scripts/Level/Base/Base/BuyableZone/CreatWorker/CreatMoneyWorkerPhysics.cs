using Signals;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class CreatMoneyWorkerPhysics : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                Debug.Log("ss");
                MoneyWorkerSignals.Instance.onGenerateMoneyWorker?.Invoke(transform);
            }
        }

    }
}