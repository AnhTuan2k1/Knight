using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Spikball : Enemy
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Bullet bulletPrefab;
    private const float maxHealth = 250;
    private float bulletSpeed = 10;
    bool isDie = false;
    private IBattleObserve battleSystem;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void Attack(int attackLevel = 1)
    {
        if (isDie) return;
        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);

        Vector3 direct = attackPoint.forward;
        direct.y += 0.05f;
        bullet.GetComponent<Rigidbody>().velocity = direct * bulletSpeed;
    }

    public override void Move()
    {
        if (isDie) return;
    }

    public override void TakeDamage(float damage, GameObject obj)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth / maxHealth);

        if (currentHealth > 0)
            PlayAnimatorGetHit(obj);
        else DestroyObj();
    }

    private void DestroyObj()
    {
        isDie = true;

        Notify();
        Destroy(gameObject);
    }

    void PlayAnimatorGetHit(GameObject obj)
    {
        
    }

    public override float AttackRange()
    {
        return 20;
    }

    public override void Attach(IBattleObserve battleObserve)
    {
        battleSystem = battleObserve;
    }
    public override void Detach(IBattleObserve battleObserve)
    {
        battleSystem = null;
    }
    public override void Notify()
    {
        if(battleSystem != null) battleSystem.Update(this);
    }
}
