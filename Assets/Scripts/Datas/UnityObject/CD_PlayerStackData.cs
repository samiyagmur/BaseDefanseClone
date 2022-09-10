using Extentions;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player/Player", menuName = "Data/PlayerDatas", order = 0)]
    public class CD_PlayerStackData : ScriptableObject
    {
        PlayerMoneyStackData playerMoneyStackData;

        PlayerAmmoStackData playerAmmoStackData;

        HostageStackData hostageStackData;

    }
}