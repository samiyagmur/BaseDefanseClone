using Abstraction;
using AIBrain;
using Data.UnityObject;
using Data.ValueObject;
using Datas.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using StateBehavior;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class AIManager : MonoBehaviour
    {

        #region MyRegion
        private AmmoManagerPropertyDepository _ammoUsers;
        private AmmoManagerPropertyDepository _enemyUsers;
        private EnemyAIData _enemyAIData;
        public Dictionary<BrainType, AmmoManagerPropertyDepository> _stateDict;
        private AmmoWorkerAIData _ammoWorkerAIData;
        private StateMachine _stateMachine;//Ayrı olabilir ayri ise dataya at
        private BrainType brainType;
        private EnemyType _enemyType;
        #endregion
        internal void GetAmmoWorkerData() => _ammoWorkerAIData = Resources.Load<CD_AIData>("Data/CD_AIData").AmmoWorkerAIDatas;

        internal void GetEnemyTypeData() => _enemyType = _enemyAIData.EnemyType;
        internal void GetEnemyData() => _enemyAIData = Resources.Load<CD_AIData>("Data/CD_AIData").EnemyAIDataList[(int)_enemyType];
        private void Awake()
        {
            GetEnemyTypeData();
            GetAmmoWorkerData();
            GetEnemyData();

        }

        //private void OnEnable() => SubscribeEvents();
        //private void SubscribeEvents()
        //{
        //    AISignals.Instance.onPlayerEnterAmmoWorkerCreaterArea+= OnPlayerEnterAmmoWorkerCreaterArea;
        //}

        //private void UnsubscribeEvents()
        //{
        //    AISignals.Instance.onPlayerEnterAmmoWorkerCreaterArea-= OnPlayerEnterAmmoWorkerCreaterArea;
        //}
        //private void OnDisable() => UnsubscribeEvents();

        private void OnPlayerEnterAmmoWorkerCreaterArea(Transform ammoWorkerCreater)
        {
            //_stateDict[brainType].CreatPoint = ammoWorkerCreater;
        }


    }
}