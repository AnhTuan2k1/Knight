


using UnityEngine;

class TrailBlueRifleBullet : Bullet
{
    public float damage = 50;
    public float bulletSpeed = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy ??= other.gameObject.GetComponentInParent<Enemy>();

            enemy.TakeDamage(damage, gameObject);
        }
    }

    public override void Spawn(Vector3 position, Quaternion rotation, Vector3 forward)
    {
        Bullet bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;
    }
}
