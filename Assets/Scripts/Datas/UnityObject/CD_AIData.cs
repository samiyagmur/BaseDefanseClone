using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level/AIData", menuName = "Data/AIData", order = 0)]
    public class CD_AIData: ScriptableObject
    {
        AmmoWorker ammoWorker;
        MoneyWorker moneyWorker;
        MineWorker mineWorkeri;
        Soldier soldier;
        Enemy enemy;
       
    }
}