using Controllers;
using Enums;
using StateBehavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Abstraction
{
    public abstract class AmmoManagerPropertyDepository :MonoBehaviour
    {

        #region AmmoWorker Fields
        private float movementSpeed;
        private Transform ammoWareHouse;
        private List<Transform> ammoWorkerStack;
        private List<GameObject> _ammoContaynerList;
        private int _isAmmoContaynerMaxAmount;
        private List<float> _ammoContaynerCurrentCount;
        private Animator animator;
        private NavMeshAgent agent;
        private GameObject _ammo;
        //private Transform _shopTransform;
        private Transform _ammoTransform;
        private CurrentTransportAmmoStatus currentAmmoTransportStatus;
        private GameObject decidedContayner;
        private GameObject ammoWorkerGameObj;
        private List<int> decideIndexList;
        private GameObject _ammoWorker;

        #endregion

        #region AmmoWorker Porparty
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public Transform AmmoWareHouse { get => ammoWareHouse; set => ammoWareHouse = value; }
        public List<Transform> AmmoWorkerStack { get => ammoWorkerStack; set => ammoWorkerStack = value; }
        public List<GameObject> AmmoContaynerList { get => _ammoContaynerList; set => _ammoContaynerList = value; }
        public int IsAmmoContaynerMaxAmount { get => _isAmmoContaynerMaxAmount; set => _isAmmoContaynerMaxAmount = value; }
        public List<float> AmmoContaynerCurrentCount { get => _ammoContaynerCurrentCount; set => _ammoContaynerCurrentCount = value; }
        //public Transform ShopTransform { get => _shopTransform; set => _shopTransform = value; }
        public NavMeshAgent Agent { get => agent; set => agent = value; }
        public GameObject Ammo { get => _ammo; set => _ammo = value; }
        public Animator Animator { get => animator; set => animator = value; }
        public Transform AmmoTransform { get => _ammoTransform; set => _ammoTransform = value; }
        public CurrentTransportAmmoStatus CurrentAmmoTransportStatus { get => currentAmmoTransportStatus; set => currentAmmoTransportStatus = value; }
        public GameObject DecidedContayner { get => decidedContayner; set => decidedContayner = value; }
        public GameObject AmmoWorkerGameObj { get => ammoWorkerGameObj; set => ammoWorkerGameObj = value; }
        public List<int> DecideIndexList { get => decideIndexList; set => decideIndexList = value; }
        public GameObject AmmoWorker { get => _ammoWorker; set => _ammoWorker = value; }

        #endregion

        internal virtual void Awake() { }

       // internal abstract void GetData();
        internal virtual void GetStatesReferences() { }
        internal virtual void TransitionofState() { }
      //  internal abstract void SetEnemyAIData();
        internal virtual void Update() { }

        

        internal virtual void SendGridInfoToStack(Vector3 orderOfLasGrid,GameObject stackObject, Transform ammoWareHouse) { }

    }
}