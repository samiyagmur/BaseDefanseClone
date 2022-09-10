using Datas.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.UnityObject
{

   
    [CreateAssetMenu(fileName = "CD_Level/Level", menuName = "Data/LevelDatas", order = 0)]
    public class CD_Level : ScriptableObject
    {

        List<LevelDatas> levelDatas;
    }
}