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

        #endregion

        #region Private Variables

        private BaseRoomData baseRoomData;

        #endregion

        #endregion

        private void Awake()
        {
            baseRoomData = GetData();
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
        #endregion  
        private BaseRoomData GetData() => InitializeDataSignals.Instance.onLoadBaseRoomData?.Invoke();

        private void SaveData() => InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(baseRoomData);

        private void SetExistingRooms()
        {
            foreach (var t in baseRoomData.Rooms.Where(t => t.SideBaseAvaliableStatus == SideBaseAvaliableStatus.Unlocked))
            {
                ChangeVisibility(t.RoomTypes);
            }
        }

        private void OnUpdateRoomData(RoomData roomData, RoomTypes roomTypes)
        {
            baseRoomData.Rooms[(int)roomTypes] = roomData;
            SaveData();
        }

        private RoomData OnSetRoomData(RoomTypes roomTypes) => baseRoomData.Rooms[(int)roomTypes];
        private void OnChangeVisibility(RoomTypes roomTypes)
        {
            ChangeVisibility(roomTypes);
            ChangeAvailabilityType(roomTypes);
        }

        private void ChangeAvailabilityType(RoomTypes roomTypes)
        {
            baseRoomData.Rooms[(int)roomTypes].SideBaseAvaliableStatus = SideBaseAvaliableStatus.Unlocked;
            InitializeDataSignals.Instance.onSaveBaseRoomData?.Invoke(baseRoomData);
        }

        private void ChangeVisibility(RoomTypes roomTypes) => extentionController.ChangeExtentionVisibility(roomTypes);

    }
}