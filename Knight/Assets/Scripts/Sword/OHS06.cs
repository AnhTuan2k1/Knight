using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

class OHS06 : Sword
{
    [SerializeField] private float damage = 200;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] float timeAtk = 0.5f;
    [SerializeField] GameObject swordTrail;

    private void OnTriggerEnter(Collider other)
    {
        //print("weapon trigger");
        print(other.gameObject.name + "---" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy"))
        {
            //print("enemy collider");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy ??= other.gameObject.GetComponentInParent<Enemy>();

            enemy.TakeDamage(damage, gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("weapon collider");
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        //print("enemy collider");
    //        collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, gameObject);
    //    }
    //}

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
        return (int)MySword.OHS06;
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

