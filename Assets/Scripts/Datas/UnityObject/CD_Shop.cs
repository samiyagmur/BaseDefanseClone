using Datas.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Shop ", menuName = "BaseDefense/CD_Shop", order = 0)]
    public class CD_Shop : ScriptableObject
    {
        List<WorkerShopData> workerShop;
        List<WeaponShopData> weaponShop;
        List<PlayerShopData> playerShop;
        SoldierShopData soldierShopDta;
    }
}