using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using System;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;

        #endregion
 

        #region Private Variables

        private PlayerData _data;
        #endregion

        #endregion

        #region GetData
        private void Awake()
        {
            _data = GetPlayerData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData").playerData;
        private void Init()
        {
            SetMovementDatas();
            SetPhysicsDatas();
            SetAnimationnDatas();
        }
        private void SetMovementDatas() => movementController.SetMovementData(_data.MovementDatas);

        private void SetPhysicsDatas() => physicsController.SetPhysicsData(_data.PhysicsDatas);

        private void SetAnimationnDatas() => animationController.SetAnimationnData(_data.PhysicsDatas);
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion

        #region Subscription methods
        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            IsExitTurret();
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
        }
        #endregion

        #region PhysicsMethods

        public void IsEnterAmmoCreater(Transform transform) => AISignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        public void IsEnterTurret(GameObject turretObj) => movementController.EnterToTurret(turretObj);
        public void IsExitTurret() => movementController.ExitToTurret();

        #endregion


    }
}
