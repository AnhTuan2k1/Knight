using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract int GetWeapon();
    public abstract bool IsMeleeWeapon();
}
