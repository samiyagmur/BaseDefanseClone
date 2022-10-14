using System.Collections.Generic;
using Data.ValueObject.LevelData;
using UnityEngine;
using Interfaces;
using Datas.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefense/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject,ISaveableEntity
    {
        public List<LevelData> LevelDatas = new List<LevelData>();

        public ShopData ShopData;

        public ScoreData ScoreData;

        public int LevelId;

        private string Key = "_levelData";

        public CD_Level(int levelId, List<LevelData> levelDatas, ShopData shopData, ScoreData scoreData)
        {
            LevelId = levelId;
            LevelDatas = new List<LevelData>(levelDatas);
            ShopData = shopData;
            ScoreData= scoreData;
        }
        public string GetKey()
        {
            return Key;
        }



    }
}