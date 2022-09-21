using Datas.ValueObject;
using System.Collections;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AmmoContayner", menuName = "Data/AmmoContaynerData")]
    public class CD_AmmoContayner : ScriptableObject 
    { 
        public AmmoContaynerData ammoContaynerData;
 
    }
}