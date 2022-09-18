using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
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
        int _line ;
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
        private PlayerData GetPlayerData() => Resources.Load<CD_PlayerData>("Data/CD_Player").PlayerDatas;
        private void Init() => SetDataToControllers();

        private void SetDataToControllers() => _movementController.SetMovementData(_data.playerMovementData); 
        #endregion

        #region Event Subscription
        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            PlayerSignal.Instance.onChangePlayerLayer += OnChangePlayerLayer;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            PlayerSignal.Instance.onChangePlayerLayer -= OnChangePlayerLayer;
        }
        private void OnDisable() => UnsubscribeEvents();

        #endregion

        private void OnChangePlayerLayer() => meshController.ChangeLayerMask();

        private void OnGetInputValues(HorizontalInputParams inputParams)
        {
            _movementController.UpdateInputValues(inputParams);
            meshController.LookRotation(inputParams);
            animationController.PlayAnimation(inputParams);
        }
    } 
}
