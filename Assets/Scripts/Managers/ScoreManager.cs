using Signals;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {

        private int totalGemValue;
        private int totalMoneyValue;

        #region EventSubscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;

            CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;

            CoreGameSignals.Instance.onGetCurrentMoney += OnGetCurrentMoney;

            CoreGameSignals.Instance.onGetCurrentDiamond += OnGetCurrentDiamond;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;

            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;

            CoreGameSignals.Instance.onGetCurrentMoney -= OnGetCurrentMoney;
            CoreGameSignals.Instance.onGetCurrentDiamond -= OnGetCurrentDiamond;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion
        private void OnUpdateGemScore(int gemValue)
        {
            totalGemValue += gemValue;
        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            totalMoneyValue += moneyValue;
        }

        private int OnGetCurrentDiamond()
        {
            return totalGemValue;
        }

        private int OnGetCurrentMoney()
        {
            return totalMoneyValue;
        }

    }
}