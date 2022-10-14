using System.Collections;
using UnityEngine;

namespace Interfaces
{
   
        public interface ICustomer
        {
            public bool HasMoney { get; set; }
            void MakePayment();

            void StopPayment();
        }
    
}