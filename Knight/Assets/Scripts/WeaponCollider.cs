using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private MeshCollider meshCollider;

    private void Start()
    {
        damage = 150;
        meshCollider.enabled = false;
    }

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

    public void StartAttack()
    {
        meshCollider.enabled = true;
    }

    public void EndAttack()
    {
        meshCollider.enabled = false;
    }
}
