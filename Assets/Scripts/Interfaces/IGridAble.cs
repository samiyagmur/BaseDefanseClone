using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IGridAble
    {
        void GanarateGrid();

        List<Vector3> LastPosition();
    }
}