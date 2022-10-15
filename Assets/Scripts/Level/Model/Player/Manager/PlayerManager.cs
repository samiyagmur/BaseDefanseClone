using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Enums.GameStates;
using Interfaces;
using Keys;
using Signals;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        
        #region Self Variables

        #region Public Variables

        public AreaType CurrentAreaType = AreaType.BaseDefense;

        public WeaponTypes WeaponType;

        public List<IDamagable> EnemyList = new List<IDamagable>();

        public Transform EnemyTarget;

        public bool HasEnemyTarget = false;

        #endregion

        #region Serialized Variables

        [SerializeField]
        private PlayerMeshController meshController;
        [SerializeField]
        private PlayerAnimationController animationController;
        [SerializeField]
        private PlayerWeaponController weaponController;
        [SerializeField]
        private PlayerShootingController shootingController;
        [SerializeField]
        private PlayerMovementController movementController;
        [SerializeField]
        private PlayerPhysicsController physicsController;
        #endregion

        #region Private Variables

        private PlayerData _data;

        private WeaponData _weaponData;

        public IDamagable DamagableEnemy;

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            _weaponData = GetWeaponData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData").playerData;
        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)WeaponType];
        private void Init() => SetDataToControllers();
        private void SetDataToControllers()
        {
            movementController.SetMovementData(_data.MovementDatas);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);

        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
            AimEnemy();
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy() => movementController.LookAtTarget(!HasEnemyTarget ? null : EnemyList[0]?.GetTransform());
        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);

    }
}

