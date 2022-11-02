using Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class BaseRoomExtentionController : MonoBehaviour
    {
        private List<GameObject> _openUpExtentions;
        private List<GameObject> _closeDownExtentions;

        public void ChangeExtentionVisibility(BaseRoomTypes baseRoomType)
        {
            _openUpExtentions[(int)baseRoomType].SetActive(true);
            _closeDownExtentions[(int)baseRoomType].SetActive(false);
        }
    }
}