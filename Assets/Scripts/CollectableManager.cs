using Concreate;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private StackableMoney stackableMoney;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => CoreGameSignals.Instance.onResetPlayerStack += OnResetPlayerStack;

        private void OnDisable() => UnsubscribeEvents();

        private void UnsubscribeEvents() => CoreGameSignals.Instance.onResetPlayerStack -= OnResetPlayerStack;

        private void OnResetPlayerStack() => stackableMoney.EditPhysics();
    }
}