

using UnityEngine;

class Player : Character
{
    [SerializeField] private const float maxHealth = 1000;

    public override void Healing(float healthPoint)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}

