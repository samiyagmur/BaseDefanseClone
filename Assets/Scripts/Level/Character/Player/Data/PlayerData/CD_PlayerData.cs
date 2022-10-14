using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "Data/PlayerData")]
    public class CD_PlayerData : ScriptableObject
    {
       public PlayerData playerData;
    }
}