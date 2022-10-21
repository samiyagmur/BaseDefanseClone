using Controller;
using Controllers;
using Controllers.PlayerControllers;
using Data.UnityObject;
using Data.ValueObject;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Interfaces;
using Keys;
using Signals;
using Sirenix.OdinInspector;
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
        [ShowInInspector]   
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
        [SerializeField]
        private PlayerHealtController playerHealtController;
        [SerializeField]
        private PlayerAccountController playerAccountController;
        [SerializeField]
        private PlayerStackerController playerMoneyStackerController;

        #endregion

        #region Private Variables

        private PlayerData _data;

        private WeaponData _weaponData;

        public IDamagable DamagableEnemy;
        private bool _canReset;

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
            playerHealtController.SetHealthData(_data.playerCharacterData);

        }
        #region Event Subscription
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange += OnDisableMovement;
            PlayerSignal.Instance.onTakePlayerDamage += OnTakePlayerDamage;
            UISignals.Instance.onChangeWeaponType += OnChangeWeaponType;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            InputSignals.Instance.onInputHandlerChange -= OnDisableMovement;
            PlayerSignal.Instance.onTakePlayerDamage -= OnTakePlayerDamage;
            UISignals.Instance.onChangeWeaponType -= OnChangeWeaponType;
        }

        private void OnChangeWeaponType(WeaponTypes type)
        {
             WeaponType = type;
            _weaponData = GetWeaponData();
            weaponController.ChangeWeaponType(_weaponData);

        }

        private void OnDisable() => UnsubscribeEvents();
        #endregion

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValues(inputParams);
            animationController.PlayAnimation(inputParams);
        
        }
        public void SetEnemyTarget()
        {
            shootingController.SetEnemyTargetTransform();
            animationController.AimTarget(true);

        }
        public void ResetPlayer()
        {
            Debug.Log("ResetPlayer");
           playerAccountController.Collider.enabled = false;
           playerMoneyStackerController.ResetStack();
           CoreGameSignals.Instance.onResetPlayerStack?.Invoke();
           DOVirtual.DelayedCall(.3f,()=>animationController.DeathAnimation());
            physicsController.ResetPlayerLayer();
           EnemyList.Clear();
           HasEnemyTarget = false;
           CheckAreaStatus(AreaType.BaseDefense);
           CoreGameSignals.Instance.onReset?.Invoke();
           OnDisableMovement(InputHandlers.None);
           DOVirtual.DelayedCall(3f, () =>
           {
               playerAccountController.Collider.enabled = true;
               UISignals.Instance.onOpenUIPanel?.Invoke(UIPanels.PlayerHealt);
               playerHealtController.IncreaseHealth();
               _canReset = false;
               transform.position = Vector3.zero;
               CoreGameSignals.Instance.onPlay?.Invoke();
               animationController.ChangeAnimations(PlayerAnimationStates.Idle);

           });
        }

        public void CheckAreaStatus(AreaType areaType) => meshController.ChangeAreaStatus(CurrentAreaType = areaType);
        private void OnDisableMovement(InputHandlers inputHandler) => movementController.DisableMovement(inputHandler);
        public void SetTurretAnim(bool onTurret) => animationController.PlayTurretAnimation(onTurret);
        internal void OpenHealtBar() => UISignals.Instance.onOpenUIPanel?.Invoke(UIPanels.PlayerHealt);
        internal void CloseHealtBar() => UISignals.Instance.onCloseUIPanel?.Invoke(UIPanels.PlayerHealt);
        private void OnTakePlayerDamage(int damage) => playerHealtController.OnTakeDamage(damage);


    }
}

