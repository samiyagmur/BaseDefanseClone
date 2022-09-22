using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilityies;

namespace Managers
{
    public class AmmoShopManager : MonoBehaviour
    {
       
        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            AmmoShopSignals.Instance.onGetAmmoContaynerGridPosList += onGetAmmoContaynerGridPosList;
            AmmoShopSignals.Instance.onGetMaxGridAmunt += OnGetInputValues;
        }

        private void UnsubscribeEvents()
        {
            AmmoShopSignals.Instance.onGetAmmoContaynerGridPosList -= onGetAmmoContaynerGridPosList;
            AmmoShopSignals.Instance.onGetMaxGridAmunt -= OnGetInputValues;
        }
        private void OnDisable() => UnsubscribeEvents();
        #endregion

        private void OnGetInputValues(float maxConteynerAmount, GameObject whichContayner)
        {
            if (whichContayner.name == "LeftContayner")
            {
                Debug.Log(maxConteynerAmount);
            }
            if (whichContayner.name == "RightContayner")
            {
                Debug.Log(maxConteynerAmount);
            }
        }

        private void onGetAmmoContaynerGridPosList(List<Vector3> contaynerList, GameObject whichContayner)
        {
           
        }


    }
}