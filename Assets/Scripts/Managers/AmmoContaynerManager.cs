using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Interfaces;
using Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilityies;

namespace Managers
{
    public class AmmoContaynerManager : MonoBehaviour
    {

        #region SelfVariables

        #region Private Variables
        private GridData _gridData; 

        private Dictionary<int, GameObject> _turrets = new Dictionary<int, GameObject>();
        #endregion

        #region Serilizefield Variebles
        [SerializeField] private IGridAble gridController;
        #endregion

        #endregion

        #region Get&SetData

        private void Awake()
        {
           // IsHitAmmoWorker();
        }
        #endregion
        private void Update()
        {
           
        }


        #region SentMomentİnfo

        internal void StackCount(int count, GameObject gameObject)
        {

            _turrets.Add(count, gameObject);
            
            _turrets = _turrets.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

        }
    
        internal void SendToTargetInfo()
        {
            if (_turrets.Count != 0)
            {
                
                AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(_turrets.ElementAt(0).Value);

            }

            else 
            {
                //Debug.Log("!!!_turrets Dictionary caunt=0 ");
                AmmoManagerSignals.Instance.onSetConteynerList?.Invoke(null);
            } 
        }

        #endregion

        #region PhysicsMethods
        public void IsHitAmmoWorker()
        {


        }

        #endregion

        #region Event Methods

        #endregion

    }
}