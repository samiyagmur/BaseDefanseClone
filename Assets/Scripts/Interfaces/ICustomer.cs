using Enums;
using System.Collections;
using UnityEngine;

namespace Interfaces
{
   
        public interface ICustomer
        {
            void PaymentAnimation(Transform paymentTransform, PoolType poolType);

            bool CanPay { get; set; }

        }
    
}