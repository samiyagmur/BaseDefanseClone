using Datas.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Datas.UnityObject
{

    [Serializable]
    [CreateAssetMenu(fileName = "CD_Level/Level", menuName = "Data/LevelDatas", order = 0)]
    public class CD_Level : ScriptableObject
    {

       public List<LevelDatas> levelDatas;
    }
}