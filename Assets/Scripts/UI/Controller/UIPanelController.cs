using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class UIPanelController : MonoBehaviour
    {   
        [SerializeField]
        List<GameObject> UIPanelsList ;
        public void OpenPanel(UIPanels panelParams)
        {
            UIPanelsList[(int)panelParams].SetActive(true);
        }
        public void ClosePanel(UIPanels panelParams)
        {
            UIPanelsList[(int)panelParams].SetActive(false);
        }
    }
}
