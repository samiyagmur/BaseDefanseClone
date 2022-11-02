using Controllers.PlayerControllers;
using Enums;
using Interfaces;
using Signals;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
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
