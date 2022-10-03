using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Sword12Purple : Sword
{
    [SerializeField] private float damage = 250;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] float timeAtk = 0.5f;
    [SerializeField] GameObject swordTrail;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy ??= other.gameObject.GetComponentInParent<Enemy>();

            enemy.TakeDamage(damage, gameObject);
        }
    }

    public override void StartAttack()
    {
        meshCollider.enabled = true;
        swordTrail.SetActive(true);
    }

    public override void EndAttack()
    {
        meshCollider.enabled = false;
        swordTrail.SetActive(false);
    }

    public override float TimeAttack()
    {
        return timeAtk;
    }

    public override int GetWeapon()
    {
        return (int)MySword.Sword12Purple;
    }

    public override bool IsMeleeWeapon()
    {
        return true;
    }

    public override void ColliderEnabled(bool enabled)
    {
        meshCollider.enabled = enabled;
    }
}
