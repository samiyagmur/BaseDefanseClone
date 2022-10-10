using Controllers;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPhysicsController : MonoBehaviour
{
    [SerializeField]
    private ShopManager _shopManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerPhysicsController playerPhyiscs))
        {
            _shopManager.IsEnterShopsForType(transform.parent.GetComponent<ShopTypeController>().GetShoopType());
            _shopManager.GetScore();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerPhysicsController playerPhyiscs))
        {
            _shopManager.IsExitAnyShops(transform.parent.GetComponent<ShopTypeController>().GetShoopType());
            _shopManager.SaveLevelID();
        }
    }
}
