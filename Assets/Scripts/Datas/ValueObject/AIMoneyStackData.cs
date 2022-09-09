using Abstraction;
using Extentions;
using System.Collections;
using UnityEngine;

namespace Datas.ValueObject
{
    public class AIMoneyStackData : StacableValue
    {
        public AIMoneyStackData(int capacity, int offset) : base(capacity, offset) { }
     
    }
}