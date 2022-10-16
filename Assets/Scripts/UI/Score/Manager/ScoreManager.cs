using Datas.ValueObject;
using Signals;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private ScoreData _scoreData;

        #region EventSubscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;

            CoreGameSignals.Instance.onUpdateMoneyScore += OnUpdateMoneyScore;

            CoreGameSignals.Instance.onUpdateGemScore += OnUpdateGemScore;

            CoreGameSignals.Instance.onGetCurrentMoney += OnGetCurrentMoney;

            CoreGameSignals.Instance.onGetCurrentDiamond += OnGetCurrentDiamond;


        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;

            CoreGameSignals.Instance.onUpdateMoneyScore -= OnUpdateMoneyScore;

            CoreGameSignals.Instance.onUpdateGemScore -= OnUpdateGemScore;

            CoreGameSignals.Instance.onGetCurrentMoney -= OnGetCurrentMoney;

            CoreGameSignals.Instance.onGetCurrentDiamond -= OnGetCurrentDiamond;

        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        private void OnLevelInitialize() => LoadScoreData();

        private void LoadScoreData() => _scoreData = InitializeDataSignals.Instance.onLoadScoreData.Invoke();

        private void OnUpdateGemScore(int gemValue)
        {
            _scoreData.Diamond += gemValue;
            UISignals.Instance.onChangeMoney?.Invoke(_scoreData.Diamond);
        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            _scoreData.Money += moneyValue;
            UISignals.Instance.onChangeDiamond?.Invoke(_scoreData.Money);
        }

        private int OnGetCurrentMoney() => _scoreData.Money;

        private int OnGetCurrentDiamond() => _scoreData.Diamond;



    }
}