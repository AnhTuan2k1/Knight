using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 5;

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    public virtual void Spawn(Vector3 position, Quaternion rotation, Vector3 forward) 
    {
        Bullet bullet = Instantiate(this, position, rotation);
        bullet.GetComponent<Rigidbody>().velocity = forward * 10;
    }
}
