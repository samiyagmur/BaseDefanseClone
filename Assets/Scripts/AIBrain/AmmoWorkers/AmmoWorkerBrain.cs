using Abstraction;
using StateBehavior;
using States;
using System;
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

            _moveToWareHouse = new MoveToWareHouse(Agent,Animator,MovementSpeed,AmmoWareHouse);

            _takeAmmo = new TakeAmmo(Agent,Animator);

            _decisionAvaliableConteyner = new DecideAvaliableTurret(Agent,Animator,MovementSpeed,AmmoContaynerList,IsAmmoContaynerMaxAmount,AmmoContaynerCurrentCount);

            _moveToAvaliableConteyner = new MoveToAvaliableContayner(Agent,Animator,MovementSpeed,DecidedContayner);

            _loadTurret = new LoadContayner();

            _fullAmmo = new FullAmmo(Agent,Animator,MovementSpeed);

            TransitionofState();
        }

        #endregion

        #region StateEngine

        internal override void TransitionofState()
        {
            Debug.Log("TransitionofState");
            _statemachine.SetState(_creat);

            #region Transtion

            At(_creat, _moveToWareHouse, IsAmmoWorkerBorn());

            At(_moveToWareHouse, _takeAmmo, WhenAmmoWorkerInAmmoWareHouse());

            At(_takeAmmo, _decisionAvaliableConteyner, WhenAmmoWorkerInAmmoWareHouse());

            At(_decisionAvaliableConteyner, _moveToAvaliableConteyner, WhenTransportationActive());

            At(_moveToAvaliableConteyner, _loadTurret, IsAmmoWorkerInContayner());

            At(_loadTurret, _moveToWareHouse, WhenAmmoDichargeStack());

            _statemachine.AddAnyTransition(_fullAmmo, WhenAmmoIsFull());

            #endregion

            #region Condition

            Func<bool> IsAmmoWorkerBorn() => () => AmmoWorker.GetComponent<AmmoWorkerBrain>().enabled==true;

            Func<bool> WhenAmmoWorkerInAmmoWareHouse() => () => AmmoWareHouse.transform.position == AmmoWorkerGameObj.transform.position
                                                   && AmmoWareHouse.transform != null;
            Func<bool> WhenTransportationActive() => () => _decisionAvaliableConteyner.SetDecidedContayner() != null;

            Func<bool> IsAmmoWorkerInContayner() => () => _decisionAvaliableConteyner.SetDecidedContayner() != null &&
                                                          (_decisionAvaliableConteyner.SetDecidedContayner().transform.position == AmmoWorkerGameObj.transform.position);

            Func<bool> WhenAmmoDichargeStack() => () => AmmoWareHouse.transform != null && _decisionAvaliableConteyner.SetDecidedContayner() == null;

            Func<bool> WhenAmmoIsFull() => () => DecideIndexList != null; 

            #endregion

            void At(IState to, IState from, Func<bool> condition) => _statemachine.AddTransition(to, from, condition);
        }

        internal override void Update() => _statemachine.Tick();


        #endregion


    }
}
