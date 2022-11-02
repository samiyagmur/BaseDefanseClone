using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class UIPanelController : MonoBehaviour
    {   
        [SerializeField]
        List<GameObject> uIPanelsList ;
        public void OpenPanel(UIPanels panelParams)
        {
            uIPanelsList[(int)panelParams].SetActive(true);
        }
        public void ClosePanel(UIPanels panelParams)
        {
            uIPanelsList[(int)panelParams].SetActive(false);
        }
    }
}
