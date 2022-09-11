using Assets.Scripts;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private PlayerMovementController _movementController;
        #endregion

        #region Private Variables

        private PlayerData _data;

        

        #endregion

        #endregion
        private void Awake()
        {
            _data = GetPlayerData();
            Init();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_Player").PlayerDatas;

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;

            Debug.Log("dd");
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void Init()
        {
            _movementController = GetComponent<PlayerMovementController>();
            SetDataToControllers();
        }
        private void SetDataToControllers()
        {
            _movementController.SetMovementData(_data.playerMovementData);
        }
     

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            _movementController.UpdateInputValues(inputParams);
            meshController.LookRotation(inputParams);
            animationController.PlayAnimation(inputParams);
        }
    } 
}
