using System;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class PlayerData 
    {
        public PlayerMovementData MovementDatas;
        public PlayerAnimationData AnimationDatas;
        public PlayerPhysicsData PhysicsDatas;

    }
}