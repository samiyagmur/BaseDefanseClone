using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

namespace Utilityies
{

    public class GridSystem : MonoBehaviour, IGridCretable
    {
        private List<Vector3> ammoPosStack;
        private float _totalCount;

        public List<Vector3> CreateGrid(GameObject instObj, Vector3 offSet, Vector3 amount)
        {
            ammoPosStack = new List<Vector3>();

            float xGrid = transform.position.x;
            float yGrid = transform.position.y;
            float zGrid = transform.position.z;

            for (float y = 0; y < offSet.y * amount.y; y += offSet.y / 2)
            {

                for (float z = 0; z < offSet.z * amount.z; z += offSet.z / 2)
                {

                    for (float x = 0; x < offSet.x * amount.x; x += offSet.x / 2)
                    {
                        //timer koy
                        Vector3 pos = new Vector3(xGrid + x, yGrid + y + 2, zGrid + z);//we pass to doPath Last pos;
                        Instantiate(instObj, pos, Quaternion.identity);
                        ammoPosStack.Add(pos);
                    }
                }
            }

            _totalCount=ammoPosStack.Count;

            return ammoPosStack;
        }

        public float MaxCount()
        {
            return _totalCount;
        }


        // Debug.Log(CreateGrid().FirstOrDefault().GetType());

    }
}
