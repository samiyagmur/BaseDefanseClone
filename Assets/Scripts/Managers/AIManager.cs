using Abstraction;
using AIBrain;
using Datas.ValueObject;
using Enums;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AIManager : MonoBehaviour
    {

        #region MyRegion
        // List<StateUsers> stateList;
        StateUsers ammoUsers;
        StateUsers enemyUsers;
        private List<StateUsers> _stateList;
        private AmmoWorkerAIData _ammoWorkerAIData;
        #endregion




        internal void GetData() => _ammoWorkerAIData = Resources.Load<CD_AIData>("Data/CD_AIData").AmmoWorkerAIDatas;

        private void Awake()
        {
            GetData();

            SetAIBrainsData();
            
        }

        private void SetAIBrainsData()
        {
            _stateList = new List<StateUsers>();

            enemyUsers = GetComponent<EnemyBrain>();
            _stateList.Add(enemyUsers);
            ammoUsers = GetComponent<AmmoWorkerBrain>();
            _stateList.Add(ammoUsers);
        }
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            AISignals.Instance.onPlayerEnterAmmoWorkerCreaterArea+= OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onPlayerEnterAmmoWorkerCreaterArea-= OnPlayerEnterAmmoWorkerCreaterArea;
        }

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform ammoWorkerCreater)
        {
            ammoUsers.CreatPoint = ammoWorkerCreater;
        }

        private void OnDisable() => UnsubscribeEvents();
    }
}