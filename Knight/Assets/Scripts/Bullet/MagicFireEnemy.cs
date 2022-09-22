using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFireEnemy : Bullet
{
    float damage = 100;
    public float bulletSpeed = 20;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.TakeDamage(damage);
        }
    }

    public override void Spawn(Vector3 position, Quaternion rotation, Vector3 forward)
    {
        Bullet bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);

        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;

        bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);
        forward = Quaternion.AngleAxis(15, Vector3.up) * forward;
        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;

        bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);
        forward = Quaternion.AngleAxis(-30, Vector3.up) * forward;
        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;
    }


}
