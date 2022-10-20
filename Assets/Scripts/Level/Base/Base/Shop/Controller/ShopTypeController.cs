using Enums;
using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class ShopTypeController : MonoBehaviour
    {
        [SerializeField]
        private UIPanels shopType;

        public UIPanels GetShoopType()
        {
            return shopType;

        }


    }
}