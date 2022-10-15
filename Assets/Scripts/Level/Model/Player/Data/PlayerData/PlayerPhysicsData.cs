using Enums;
using Enums.GameStates;
using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class PlayerPhysicsData 
    {

        public AreaType _playerLayersType;
        public int _desitionAngle = 90;

    }
}