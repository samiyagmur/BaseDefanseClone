using System.Collections;
using UnityEngine;

namespace Interfaces
{

    internal interface IDamager
    {

        int Damage { get; set; }

        int GetDamage();
    }
    
}