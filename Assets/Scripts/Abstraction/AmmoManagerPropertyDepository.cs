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


        internal virtual void Awake() { }

        internal virtual void GetStatesReferences() { }
        internal virtual void TransitionofState() { }

        internal virtual void Update() { }

        internal virtual void SendContanerInfos(List<GameObject> ammoContaynerList, int isAmmoContaynerMaxAmount, List<float> ammoContaynerCurrentCount) { }

        internal virtual void SendStackInfos(int isAmmoContaynerMaxAmount) { }

        internal virtual void SendTriggerInfo(bool IsInPlaceWareHouse) { }

        internal virtual void ChechToConteynerFull(List<GameObject> ammoContaynerList, int isAmmoContaynerMaxAmount, List<float> ammoContaynerCurrentCount) { }
   





    }
}