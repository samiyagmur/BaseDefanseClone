using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{   
    [CreateAssetMenu(fileName = "CD_Player", menuName = "Data/PlayerData", order = 0)]
    public class CD_PlayerData : ScriptableObject
    {
        
        public PlayerData PlayerDatas=new PlayerData();
        
    }
}