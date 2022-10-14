using Enums;
using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class PlayerMovementData 
    {

        public float Speed;

        public float ExitClampLeftSide = -0.3f;

        public float ExitClampRightSide = +0.3f;

        public float ExitClampBackSide = -0.6f;
    }
}