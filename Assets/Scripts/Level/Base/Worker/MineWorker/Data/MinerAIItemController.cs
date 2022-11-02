using Enum;
using Enums;
using Interfaces;
using Signals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public class MinerAIItemController : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Public Variables

        public Dictionary<MinerItems, GameObject> ItemList = new Dictionary<MinerItems, GameObject>();

        #endregion Public Variables

        #region Serialized Variables

        [SerializeField] private GameObject pickaxe;
        [SerializeField] private GameObject gem;
        [SerializeField] private Transform gemHolder;

        #endregion Serialized Variables

        #endregion Self Variables

        private void Awake()
        {
            AddToDictionary();
            CloseAllObject();
        }

        private void CloseAllObject()
        {
            for (int index = 0; index < ItemList.Count; index++)
            {
                ItemList.ElementAt(index).Value.SetActive(false);
            }
        }

        private void AddToDictionary()
        {
            ItemList.Add(MinerItems.Gem, gem);
            ItemList.Add(MinerItems.Pickaxe, pickaxe);
        }

        public void OpenItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
                ItemList[currentItem].SetActive(true);
            }
            if (MinerItems.None == currentItem)
            {
                foreach (Transform child in gemHolder)
                {
                    Destroy(child.gameObject);
                }
            }
            if (MinerItems.Gem == currentItem)
            {
                gem = GetObject(PoolType.Gem);
                gem.transform.parent = gemHolder;
                //gem.Cop
                gem.transform.localPosition = Vector3.zero;
                gem.transform.localScale = Vector3.one * 3;
                gem.transform.localRotation = Quaternion.identity;
            }
        }

        public void CloseItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
                ItemList[currentItem].SetActive(false);
            }
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool(poolName);
        }
    }
}