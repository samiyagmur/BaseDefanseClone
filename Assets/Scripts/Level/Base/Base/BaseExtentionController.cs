using Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BaseExtentionController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> OpenUpExtentions;

        [SerializeField]
        private List<GameObject> CloseDownExtentions;

        [SerializeField]
        private List<GameObject> SideOpenClose;

        public void ChangeExtentionVisibility(RoomTypes roomTypes)
        {
            SideOpenClose[(int)roomTypes].SetActive(false);
            OpenUpExtentions[(int)roomTypes].SetActive(true);
            CloseDownExtentions[(int)roomTypes].SetActive(false);

            if (SideOpenClose.Count > (int)roomTypes + 2)
            {
                SideOpenClose[(int)roomTypes + 2].SetActive(true);
            }
        }
    }
}