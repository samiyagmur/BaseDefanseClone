using Extentions;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player/PlayerStack", menuName = "Data/PlayerStackData", order = 0)]
    public class CD_PlayerStackData : ScriptableObject
    {
        public  PlayerMoneyStackData playerMoneyStackData;

        public  PlayerAmmoStackData playerAmmoStackData;

        public  HostageStackData hostageStackData;

    }
}