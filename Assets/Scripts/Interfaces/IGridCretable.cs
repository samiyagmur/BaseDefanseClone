using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGridCretable
    {
         List<Vector3> CreateGrid(GameObject instObj, Vector3 offSet, Vector3 amount);

         float MaxCount();
    }
}