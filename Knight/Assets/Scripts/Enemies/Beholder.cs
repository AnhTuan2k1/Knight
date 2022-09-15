using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Beholder : Enemy
{
    [SerializeField] private Animator beholderAni;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Bullet bulletPrefab;
    private const float maxHealth = 200;

    private static string getHit = "GetHit";
    private static string die = "Die";
    private static string attack = "Attack01";
    private static string walk = "WalkFWD";
    private float bulletSpeed = 10;

    bool isDie = false;
    float timeDie = 1.8f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void Attack(int attackLevel = 1)
    {
        if (isDie) return;
        Bullet bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        bullet.GetComponent<Transform>().Rotate(new Vector3(1, 0, 0), -90, Space.Self);

        bullet.GetComponent<Rigidbody>().velocity = attackPoint.forward * bulletSpeed;

        beholderAni.Play(attack);
    }

    public override void Move()
    {
        if (isDie) return;
        beholderAni.Play(walk);
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

        gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
        beholderAni.Play(die);

        Invoke(nameof(PauseAni), 0.7f);

        Destroy(gameObject, timeDie);
    }

    private void PauseAni()
    {
        beholderAni.speed = 0;
    }

    void PlayAnimatorGetHit(GameObject obj)
    {
        beholderAni.Play(getHit);
    }

    public override float AttackRange()
    {
        return 20;
    }

}