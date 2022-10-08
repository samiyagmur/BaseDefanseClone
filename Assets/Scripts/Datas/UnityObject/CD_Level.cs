using System.Collections.Generic;
using Data.ValueObject.LevelData;
using UnityEngine;
using Interfaces;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefense/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject,ISaveableEntity
    {
        public List<LevelData> LevelDatas = new List<LevelData>();

        public int LevelId;

        public string Key = "LevelData";
        public CD_Level(int levelId, List<LevelData> levelDatas)
        {
            LevelId = levelId;
            LevelDatas = new List<LevelData>(levelDatas);
        }
        public string GetKey()
        {
            return Key;
        }



    }
}