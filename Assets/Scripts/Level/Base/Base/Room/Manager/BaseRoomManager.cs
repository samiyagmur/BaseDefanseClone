using Data.ValueObject;
using Data.ValueObject.LevelData;
using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class BaseRoomManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        private BaseRoomData _data;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        #region Event Subscription

   
        #endregion

        private void OnLoadBaseRoomData(BaseRoomData data)
        {
            _data = data;
        }





    }
}