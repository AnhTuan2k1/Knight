using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : Bullet
{
    public float damage = 50;
    public float bulletSpeed = 20;
    public GameObject burstObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy ??= other.gameObject.GetComponentInParent<Enemy>();

            enemy.TakeDamage(damage, gameObject);

            Instantiate(burstObj,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(0, 0, 0, transform.rotation.w));
            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("BulletExplosion", transform.position);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Instantiate(burstObj,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(0, 0, 0, transform.rotation.w));
            Destroy(gameObject);

            FindObjectOfType<AudioManager>().Play("BulletExplosion", transform.position);
        }
    }

    public override void Spawn(Vector3 position, Quaternion rotation, Vector3 forward)
    {      
        Bullet bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);

        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;
        //bullet.GetComponent<Rigidbody>().AddForce(forward * bulletSpeed);
    }
}
