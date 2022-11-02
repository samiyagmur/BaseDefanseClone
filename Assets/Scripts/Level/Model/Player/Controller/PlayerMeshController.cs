using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables



        #region Serialized Variables,

        [SerializeField] private Transform manager;
        [SerializeField] private MeshRenderer weaponMeshRenderer;
        [SerializeField] private MeshRenderer sideMeshRenderer;

        #endregion Serialized Variables,

        #region Private Variables

        private WeaponData _data;

        #endregion Private Variables

        #endregion Self Variables

        public void SetWeaponData(WeaponData weaponData)
        {
            _data = weaponData;
        }

        public void ChangeAreaStatus(AreaType areaStatus)
        {
            if (areaStatus == AreaType.BaseDefense)
            {
                weaponMeshRenderer.enabled = false;
                sideMeshRenderer.enabled = false;
            }
            else
            {
                weaponMeshRenderer.enabled = true;
                sideMeshRenderer.enabled = _data.HasSideMesh;
            }
        }
    }
}