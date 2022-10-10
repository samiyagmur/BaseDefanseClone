using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class UIPanelController : MonoBehaviour
    {   


        [SerializeField]
        List<GameObject> UIPanelsList = new List<GameObject>();
        public void OpenPanel(ShopType panelParams)
        {
            
            UIPanelsList[(int)panelParams].SetActive(true);
        }

        public void ClosePanel(ShopType panelParams)
        {
            UIPanelsList[(int)panelParams].SetActive(false);
        }
    }
}
