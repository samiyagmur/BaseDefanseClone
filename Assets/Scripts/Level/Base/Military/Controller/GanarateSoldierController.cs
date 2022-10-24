using Controllers;
using Signals;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Level.Base.Military.Controller
{
    public class NewMonoBehaviour : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController playerPhysicsController))
            {
                AISignals.Instance.onGenerateSoldier?.Invoke();
            }
        }
    }
}