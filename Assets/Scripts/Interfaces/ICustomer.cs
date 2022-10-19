using System.Collections;
using UnityEngine;

namespace Interfaces
{
   
        public interface ICustomer
        {

            void PlayPaymentAnimation(Transform transform);

            bool CanPay { get; set; }

        }
    
}