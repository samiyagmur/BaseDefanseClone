using Data.ValueObject;
using UnityEngine;

namespace Controllers
{
    public class PlayerWeaponController : MonoBehaviour
    {
         #region Self Variables
        
         #region Public Variabl
         #endregion
     
         #region Serialized Variables
         
         [SerializeField] private MeshFilter meshFilter;
         [SerializeField] private MeshFilter sideMeshFilter;
         #endregion
     
         #region Private Variabl
         private bool _hasSideMesh;
         private int _damage;
         private float _attackRate;
         private int _weaponLevel;
         #endregion
         #endregion

         public void SetWeaponData(WeaponData weaponData)
         {
             SetData(weaponData);
         }
         private void SetData(WeaponData weaponData)
         {
             meshFilter.mesh = weaponData.WeaponMesh;
             _damage = weaponData.Damage;
             _attackRate = weaponData.AttackRate;
             _weaponLevel = weaponData.WeaponLevel;
             _hasSideMesh = weaponData.HasSideMesh;
             if (!weaponData.HasSideMesh) return;
             sideMeshFilter.mesh = weaponData.SideMesh;
         }
         public void ChangeWeaponType(WeaponData weaponData)
         {
            Debug.Log("ChangeWeaponType");
            SetData(weaponData);
         }
    }
}