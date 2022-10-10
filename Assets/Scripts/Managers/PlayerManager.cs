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
        public AreaType currentAreaType = AreaType.BaseDefense;
        public WeaponTypes WeaponType;

        public List<IDamagable> EnemyList = new List<IDamagable>();
        public Transform EnemyTarget;
        public bool HasEnemyTarget = false;

        public IDamagable DamagableEnemy;
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerWeaponController weaponController;
        [SerializeField] private PlayerShootingController shootingController;

        #endregion


        #region Private Variables
        private WeaponData _weaponData;

        private PlayerData _data;
        #endregion

        #endregion

        #region GetData
        private void Awake()
        {
            _weaponData = GetWeaponData();
            _data = GetPlayerData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_PlayerData").playerData;

        private WeaponData GetWeaponData() => Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData[(int)WeaponType];
        private void Init()
        {

/* Unmerged change from project 'Assembly-CSharp.Player'
Before:
            SetDataToControllers();
After:
            SetDataToControllers(Get_weaponData());
*/
            SetDataToControllers(Get_weaponData());
            SetPhysicsDatas();

        }

        private WeaponData Get_weaponData()
        {
            return _weaponData;
        }

        private void SetDataToControllers(WeaponData _weaponData)
        {
            movementController.SetMovementData(_data.MovementDatas);
            weaponController.SetWeaponData(_weaponData);
            meshController.SetWeaponData(_weaponData);
        }

        private void SetPhysicsDatas() => physicsController.SetPhysicsData(_data.PhysicsDatas);


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
            

            if (!HasEnemyTarget) return;
            AimEnemy();
        }
        public void CheckAreaStatus(AreaType AreaStatus)
        {
            currentAreaType = AreaStatus;
            meshController.ChangeAreaStatus(AreaStatus);
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);
            AimEnemy();
        }
        private void AimEnemy()
        {
            if (EnemyList.Count != 0)
            {
                var transformEnemy = EnemyList[0].GetTransform();
               // movementController.RotateThePlayer(transformEnemy);
            }
        }

        #endregion


        #region PhysicsMethods

        public void IsEnterAmmoCreater(Transform transform) => AmmoManagerSignals.Instance.onPlayerEnterAmmoWorkerCreaterArea(transform);
        public void IsEnterTurret(GameObject turretObj) => movementController.EnterToTurret(turretObj);
        public void IsExitTurret() => movementController.ExitToTurret();

        #endregion


    }
}

