using Enums;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {
        public StackingSystem StackingSystem;

        [ShowIf("StackingSystem", Enums.StackingSystem.Static)]
        public List<GridData> StaticGridDatas;

        [ShowIf("StackingSystem", Enums.StackingSystem.Dynamic)]
        public List<GridData> DynamicGridDatas;
    }
}