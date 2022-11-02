using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PortalManager : MonoBehaviour
    {
        #region Self variables



        #region Seriliazable Variables

        [SerializeField]
        private PortalController portalController;

        #endregion Seriliazable Variables

        #endregion Self variables

        #region Event Subscriptions

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => EnemySignals.Instance.onOpenPortal += OnOpenPortal;

        private void UnsubscribeEvents() => EnemySignals.Instance.onOpenPortal -= OnOpenPortal;

        private void OnDisable() => UnsubscribeEvents();

        private void OnOpenPortal() => portalController.OpenPortal();

        #endregion Event Subscriptions
    }
}