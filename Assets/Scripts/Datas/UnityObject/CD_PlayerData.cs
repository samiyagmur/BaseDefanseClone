using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player/Player", menuName = "Data/PlayerDatas", order = 0)]
    public class CD_PlayerData : MonoBehaviour
    {
        public int Healt;
        public float Speed;
        public float AtackRange;

    }
}