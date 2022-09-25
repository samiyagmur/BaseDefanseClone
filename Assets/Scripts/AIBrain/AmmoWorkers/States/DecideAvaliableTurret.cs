using Abstraction;
using Enums;
using Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


namespace States
{
    public class DecideAvaliableTurret :IState
    {
        #region Self Private Variables

        private List<int> _decideIndexList;
        private GameObject _decidedContaner;
        #endregion

        #region Constractors

        private NavMeshAgent _agent;
        private Animator _animator;
        private float _movementSpeed;
        private List<GameObject> _ammoContaynerList;
        private int _isAmmoContaynerMaxAmount;
        private List<float> _ammoContaynerCurrentCount;

        public DecideAvaliableTurret(NavMeshAgent agent, Animator animator, float movementSpeed, List<GameObject> ammoContaynerList, int isAmmoContaynerMaxAmount, List<float> ammoContaynerCurrentCount)
        {
            _agent = agent;
            _animator = animator;
            _movementSpeed = movementSpeed;
            _ammoContaynerList = ammoContaynerList;
            _isAmmoContaynerMaxAmount = isAmmoContaynerMaxAmount;
            _ammoContaynerCurrentCount = ammoContaynerCurrentCount;
        }

        #endregion

        #region States
        public void Enter()
        {

            _decideIndexList = new List<int>();

            Debug.Log("DecideAvaliableTurret");

            for (int i = 0; i < _ammoContaynerList.Count; i++)
            {
                if (_isAmmoContaynerMaxAmount != _ammoContaynerCurrentCount[i])
                {
                    _decideIndexList.Add(i);
                }
            }

            int decideMinIndex = _decideIndexList.Min();
            _decidedContaner = _ammoContaynerList[decideMinIndex];




        }

        public void Exit()
        {

        }

        public void Tick()
        {

        }

        #endregion

        #region ToBeSent
        public GameObject SetDecidedContayner()
        {
            return _decidedContaner;
        } 
        #endregion//buraya bak
    }
}