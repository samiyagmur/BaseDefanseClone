using System;
using Interfaces;
using Controllers;
using UnityEngine;

namespace Managers
{
    public class MineManager : MonoBehaviour, IDamager

    {

        #region Self Variables

        #region Public Variables

        public bool IsPayedTotalAmount => (_payedGemAmount >= requiredGemAmount);

        public int _damage { get; set ; }

        public int GemAmount; //Sinyalle Cekilecek Score Manager Uzerinden
        public int LureTime = 5;
        public int MineCountDownTime = 60;
        [SerializeField] private int explosionDamage = 999;

        #endregion

        #region Serialized Variables

        [SerializeField] private MinePhysicsController minePhysicsController;
        [SerializeField] private int requiredGemAmount;

        #endregion

        #region Private Variables

        private int _payedGemAmount = 0;
        //private BombData Data;



        #endregion

        #endregion
        private void Awake()
        {
            //Data= GetMineData();
        }

        //private BombData GetMineData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrontyardData.Bomb[0];
        public void ShowGemAmountText()
        {

        }

        public void LureColliderState(bool _state)
        {
            if (_state == true)
                _payedGemAmount = 0;

            minePhysicsController.LureColliderState(_state);
        }

        public void ExplosionColliderState(bool _state)
        {
            minePhysicsController.ExplosionColliderState(_state);
        }

        public void PayGemToMine()
        {
            GemAmount--;
            if (GemAmount<=0)
            {
                GemAmount = 0;
            }
            _payedGemAmount++;

        }

        public int GetDamage()
        {
            return explosionDamage;
        }

    }
}