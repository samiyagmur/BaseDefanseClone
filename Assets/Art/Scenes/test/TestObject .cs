using System.Collections;
using UnityEngine;

namespace Assets.Art.Scenes.test
{
    public class TestObject : MonoBehaviour, ITestable
    {
        public GameObject SetScript()
        {
            return gameObject;
        }
    }
}