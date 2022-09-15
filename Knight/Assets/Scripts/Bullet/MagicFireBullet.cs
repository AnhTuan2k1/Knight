using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireBullet : Bullet
{
    float damage = 100;
    float lifeTime = 5;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.TakeDamage(damage);
        }
    }
}
