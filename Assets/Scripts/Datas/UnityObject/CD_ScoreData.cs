using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Score/Score", menuName = "Data/ScoreDatas", order = 0)]
    public class CD_ScoreData : ScriptableObject
    {
        ScoreData scoreData;
    }
}