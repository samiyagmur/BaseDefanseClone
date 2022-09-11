using Datas.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObject
{

    [CreateAssetMenu(fileName = "CD_Weapon/Weapon", menuName = "Data/WeaponDatas", order = 0)]
    public class CD_WeaponData : ScriptableObject
    {
        public List<WeaponData> weapons;
        
    }
}