using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_InputData", menuName = "Data/InputData")]
    public class CD_InputData : ScriptableObject
    {
        public InputData InputDatas;
    }
}