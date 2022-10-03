


using UnityEngine;

class TrailBlueRifleBullet : Bullet
{
    public float damage = 50;
    public float bulletSpeed = 50;
    public Vector3 lastVelocity;
    public Rigidbody rb;
    public GameObject burstObj;

    private void Start()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //damages enemy
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy ??= other.gameObject.GetComponentInParent<Enemy>();
            enemy.TakeDamage(damage, gameObject);

            // burst affect
            Instantiate(burstObj,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(0, 0, 0, transform.rotation.w));

            FindObjectOfType<AudioManager>().Play("ElectricAffectSound", transform.position);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            //bullet bounces
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, other.bounds.center.normalized);
            transform.rotation = Quaternion.LookRotation(direction);
            GetComponent<Rigidbody>().velocity = direction * Mathf.Max(speed, 0f);

            Instantiate(burstObj,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(0, 0, 0, transform.rotation.w));

            FindObjectOfType<AudioManager>().Play("ElectricAffectSound", transform.position);
        }
    }

    public override void Spawn(Vector3 position, Quaternion rotation, Vector3 forward)
    {
        Bullet bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Rigidbody>().velocity = forward * bulletSpeed;
    }
}
