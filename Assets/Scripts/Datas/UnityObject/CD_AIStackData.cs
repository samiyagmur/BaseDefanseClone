using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{   
    [CreateAssetMenu(fileName = "CD_AIStack/AIStack", menuName = "Data/AIStackDatas", order = 0)]
    public class CD_AIStackData : ScriptableObject
    {
        AIMoneyStackData aIMoneyStackData;
        AIAmmoStackData aIAmmoStackData;
    }
}