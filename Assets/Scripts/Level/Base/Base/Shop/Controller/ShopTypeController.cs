using Enums;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ShopTypeController : MonoBehaviour
    {
        [SerializeField]
        private ShopType shopType;

        public ShopType GetShoopType()
        {
            return shopType;

        }


    }
}