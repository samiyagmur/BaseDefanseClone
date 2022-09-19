using Interfaces;
using Signals;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class delete : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(typeof(IGeterGameObject), out Component getterGameObject))
            {
                TurretSignals.Instance.onPressTurretButton?.Invoke();
            }
        }
    }
}