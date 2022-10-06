using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InsitializeLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
        }
    }
}