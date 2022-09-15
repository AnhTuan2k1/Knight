using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikball : Enemy
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Bullet bulletPrefab;
    private float bulletSpeed = 10;

    public override void Attack(int attackLevel = 1)
    {
        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);

        Vector3 direct = attackPoint.forward;
        direct.y += 0.05f;
        bullet.GetComponent<Rigidbody>().velocity = direct * bulletSpeed;
    }

    public override void Move()
    {
       
    }

    public override void TakeDamage(float damage, GameObject obj)
    {

    }

    public override float AttackRange()
    {
        return 20;
    }
}
