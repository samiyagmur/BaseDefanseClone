using Data.ValueObject;
using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AIData", menuName = "Data/AIData")]
    public class CD_AIData : ScriptableObject
    {
        public EnemyAIData EnemyAIData;
        public AmmoWorkerAIData AmmoWorkerAIDatas;
        public SoldierAIData SoldierAIDatas;
    }
}