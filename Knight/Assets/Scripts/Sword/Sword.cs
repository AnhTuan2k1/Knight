using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Sword : Weapon
{
    public abstract void EndAttack();
    public abstract void StartAttack();
    public virtual float TimeAttack() => 0;
}
