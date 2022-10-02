using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon
{
    [SerializeField] protected Transform attackPoint;

    public abstract void Shoot(Transform target = null, float AdjustHeight = 0);
}
