using Controller;
using Data.ValueObject;
using Enums;
using Signals;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class BaseManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BaseExtentionController extentionController;

        #endregion Serialized Variables

        #region Private Variables

        private BaseRoomData _baseRoomData;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            _baseRoomData = GetData();
            SetExistingRooms();
        }

        #region Event Subscription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility += OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData += OnSetRoomData;
            BaseSignals.Instance.onUpdateRoomData += OnUpdateRoomData;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onChangeExtentionVisibility -= OnChangeVisibility;
            BaseSignals.Instance.onSetRoomData -= OnSetRoomData;
            BaseSignals.Instance.onUpdateRoomData -= OnUpdateRoomData;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subscription

        private BaseRoomData GetData() => InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke();

        private void SaveData() => InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(_baseRoomData);

        private void SetExistingRooms()
        {
            foreach (var t in _baseRoomData.Rooms.Where(t => t.SideBaseAvaliableStatus == SideBaseAvaliableStatus.Unlocked))
            {
                ChangeVisibility(t.RoomTypes);
            }
        }

        private void OnUpdateRoomData(RoomData roomData, RoomTypes roomTypes)
        {
            _baseRoomData.Rooms[(int)roomTypes] = roomData;
            SaveData();
        }

        private RoomData OnSetRoomData(RoomTypes roomTypes) => _baseRoomData.Rooms[(int)roomTypes];

        private void OnChangeVisibility(RoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
            ChangeAvailabilityType(roomTypes);
        }

        private void ChangeAvailabilityType(RoomTypes roomTypes)
        {
            _baseRoomData.Rooms[(int)roomTypes].SideBaseAvaliableStatus = SideBaseAvaliableStatus.Unlocked;
            InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(_baseRoomData);
        }

        private void ChangeVisibility(RoomTypes roomTypes) => extentionController.ChangeExtentionVisibility(roomTypes);
    }
}