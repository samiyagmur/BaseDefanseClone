using System.Collections;
using UnityEngine;

namespace Interfaces
{

    internal interface IDamager
    {

        int _damage { get; set; }

        int GetDamage();
    }
    
}