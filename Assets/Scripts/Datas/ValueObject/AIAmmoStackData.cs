using Abstraction;
using Extentions;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    public class AIAmmoStackData : StacableValue
    {
        public AIAmmoStackData(int capacity, int offset) : base(capacity, offset) { }
      
    }
}