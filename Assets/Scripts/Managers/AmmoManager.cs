using Interfaces;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{


    public class AmmoManager : MonoBehaviour, GetterConteynerInfo
    {
        #region Self-Private Variabels

        private List<GameObject> _allConteynerList = new List<GameObject>();
        private int _ısContaynerMaxValue;
        private List<float> _listContaynerCurrentAmount = new List<float>();

        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {

            AmmoManagerSignals.Instance.onContaynerStackFull += OnContaynerFull;
            AmmoManagerSignals.Instance.onSetConteynerList += OnSetConteynerList;
            AmmoManagerSignals.Instance.onCurrentContaynerAmount += OnCurrentContaynerAmount;

        }

        private void UnsubscribeEvents()
        {
            AmmoManagerSignals.Instance.onContaynerStackFull -= OnContaynerFull;
            AmmoManagerSignals.Instance.onSetConteynerList -= OnSetConteynerList;
            AmmoManagerSignals.Instance.onCurrentContaynerAmount -= OnCurrentContaynerAmount;

        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region Subscirabe Event methods
        private void OnSetConteynerList(GameObject conteyner) => _allConteynerList.Add(conteyner);

        private void OnContaynerFull(int IsConteynerMax) => _ısContaynerMaxValue = IsConteynerMax;

        private void OnCurrentContaynerAmount(float currentContaynerAmount) => _listContaynerCurrentAmount.Add(currentContaynerAmount);
        #endregion

        #region Physics Methods
        internal void IsAmmoWorkerHit() => AmmoManagerSignals.Instance.onGetCurrentContaynerInfo();
        #endregion

        #region SendInfo
        internal void IsNewList()//this's so importent :)
        {
            _allConteynerList = new List<GameObject>();
            _listContaynerCurrentAmount = new List<float>();
        }

        public List<GameObject> GetterConteynerList() => _allConteynerList;

        public int GetterConteynerMaxAmunt() => _ısContaynerMaxValue;

        public List<float> GetterConteynerCurrentAmunt() => _listContaynerCurrentAmount; 

        #endregion
    }
}