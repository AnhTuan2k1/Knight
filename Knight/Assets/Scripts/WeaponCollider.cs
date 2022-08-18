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
        print("enter " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
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
