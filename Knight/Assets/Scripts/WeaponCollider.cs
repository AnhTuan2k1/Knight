using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] private float damage;
    private MeshCollider meshCollider;

    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("weapon collider");
        if (other.gameObject.CompareTag("Enemy"))
        {
            //print("enemy collider");
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage, gameObject);
        }
    }

    public void StartAttack()
    {
        meshCollider.enabled = true;
    }

    public void EndAttack()
    {
        meshCollider.enabled = false;
    }
}
