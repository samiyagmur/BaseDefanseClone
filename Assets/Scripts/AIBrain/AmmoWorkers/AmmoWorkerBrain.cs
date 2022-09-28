using Abstraction;
using StateBehavior;
using States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIBrain
{
    public class AmmoWorkerBrain : AmmoManagerPropertyDepository
    {
        #region Self Variables

        #region SerilizeField Variables

        #endregion

        #region Private Variables 

        #region State Field

        private MoveToWareHouse _moveToWareHouse;
        private TakeAmmo _takeAmmo;
        private DecideAvaliableTurret _decisionAvaliableConteyner;
        private MoveToAvaliableContayner _moveToAvaliableConteyner;
        private LoadContayner _loadTurret;
        private FullAmmo _fullAmmo;
        private Creat _creat;
        #endregion

        private StateMachine _statemachine;

        #endregion

        #endregion              

        #region GetReferans
        internal override void Awake() => GetStatesReferences();


        internal override void GetStatesReferences()
        {
            _statemachine = new StateMachine();

            _creat = new Creat();

            _moveToWareHouse = new MoveToWareHouse(Agent,Animator,MovementSpeed,AmmoWareHouse,AmmoWorker);

            _takeAmmo = new TakeAmmo(Agent,Animator);

            _decisionAvaliableConteyner = new DecideAvaliableTurret(Agent,Animator,MovementSpeed);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(Agent,Animator,MovementSpeed);

            _loadTurret = new LoadContayner(Agent, Animator, MovementSpeed, AmmoWareHouse);

            _fullAmmo = new FullAmmo(Agent,Animator,MovementSpeed);

            TransitionofState();
        }

        internal override void SendContanerInfos(List<GameObject> ammoContaynerList, int isAmmoContaynerMaxAmount, List<float> ammoContaynerCurrentCount)
        {
            _decisionAvaliableConteyner.SetData(ammoContaynerList, isAmmoContaynerMaxAmount, ammoContaynerCurrentCount);
            
        }

        internal override void SendStackInfos(Transform ammoWareHouse, int isAmmoContaynerMaxAmount,Transform ammoWorker)
        {
           
            _takeAmmo.SetData( ammoWareHouse, isAmmoContaynerMaxAmount, ammoWorker);
        }

        #endregion

        #region StateEngine

        internal override void TransitionofState()
        {   

            #region Transtion

            At(_creat, _moveToWareHouse, IsAmmoWorkerBorn());

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _decisionAvaliableConteyner, WhenAmmoWorkerStackFull());

            At(_decisionAvaliableConteyner, _moveToAvaliableConteyner, WhenTransportationActive());

            At(_moveToAvaliableConteyner, _loadTurret, IsAmmoWorkerInContayner());

            At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());

            _statemachine.AddAnyTransition(_fullAmmo, WhenAmmoIsFull());

            _statemachine.SetState(_creat);

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);

            #endregion

            #region Condition

            Func<bool> IsAmmoWorkerBorn() => () => AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => InplaceWorker && AmmoWareHouse.transform != null;

            Func<bool> WhenAmmoWorkerStackFull() => () => _takeAmmo.IsStackFull() == Enums.PlayerAmmaStackStatus.Full;

            Func<bool> WhenTransportationActive() => () => _decisionAvaliableConteyner.SetDecidedContayner() != null;//takemammocalýscak

            Func<bool> IsAmmoWorkerInContayner() => () =>_decisionAvaliableConteyner.SetDecidedContayner() != null &&
                                                          (_decisionAvaliableConteyner.SetDecidedContayner().transform.position == AmmoWorkerGameObj.transform.position);

            Func<bool> WhenAmmoDichargeStack() => () => AmmoWareHouse.transform != null && _decisionAvaliableConteyner.SetDecidedContayner() == null;

            Func<bool> WhenAmmoIsFull() => () => DecideIndexList != null;

            #endregion
        }
        
        internal override void Update()
        {
            SendDecidedGameObject();
            _statemachine.Tick();
        }

        private void SendDecidedGameObject()
        {  
            if (_decisionAvaliableConteyner.SetDecidedContayner()&& InplaceWorker)
            {
                Debug.Log(PlayerAmmaStackStatus);
                _moveToAvaliableConteyner.SetData(_decisionAvaliableConteyner.SetDecidedContayner());
                
            }
        }


        #endregion


    }
}
