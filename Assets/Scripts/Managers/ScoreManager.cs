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

        //private void OnApplicationPause(bool pause)
        //{
           
        //    InitializeDataSignals.Instance.onSaveScoreData?.Invoke(_scoreData);
        //}

        private void OnLevelInitialize()
        {
            LoadScoreData();
            SendScoreData();
        }

        private void LoadScoreData()
        {
            _scoreData = InitializeDataSignals.Instance.onLoadScoreData.Invoke();
        }
        private void SendScoreData()
        {
            CoreGameSignals.Instance.onUpdateMoneyScore?.Invoke(_scoreData.Money);
            CoreGameSignals.Instance.onUpdateGemScore?.Invoke(_scoreData.Diamond);
        }

        private void OnUpdateGemScore(int gemValue)
        {
            _scoreData.Money += gemValue;
        }

        private void OnUpdateMoneyScore(int moneyValue)
        {
            //_scoreData.Diamond += moneyValue;
        }
        private int OnGetCurrentMoney()
        {
            return _scoreData.Money;
        }
        private int OnGetCurrentDiamond()
        {
            return _scoreData.Diamond;
        }



    }
}