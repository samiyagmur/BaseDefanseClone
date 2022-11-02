using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class BombPhysicController : MonoBehaviour, IDamager
{
    private const int _damage=40;

    public int Damage { get; set  ; }

    public int GetDamage()
    {
        return _damage;
    }
}
