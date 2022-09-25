using System;
using System.Collections;
using UnityEngine;
using Interfaces;
using Enums;

namespace Managers
{
    public class CreatWorkerManager : MonoBehaviour,IGetPoolObject
    {

        public void IsPlayerHit()
        {
           var obj = GetObject(PoolType.AmmoWorkerAI.ToString());

            obj.transform.position = this.transform.position;
        }

        public GameObject GetObject(string poolName)
        {
          return  ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
        }

    }
}