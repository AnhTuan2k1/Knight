

using System;
using System.Collections;
using UnityEngine;

public class Turtle : Enemy
{

    Animator turtleAni;
    private const float maxHealth = 1000;
    private static string getHit = "GetHit";
    private static string attack1 = "Attack01";
    private static string attack2 = "Attack02";
    private static string walk = "WalkFWD";
    private static string die = "Die";
    bool isDie = false;

    private void Start()
    {
        turtleAni = GetComponentInParent<Animator>();
        currentHealth = maxHealth;
    }

    public override void Healing(float healthPoint)
    {
        currentHealth += healthPoint;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealth(currentHealth / maxHealth);
    }

    public override void TakeDamage(float damage, GameObject obj)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth / maxHealth);

        if (currentHealth > 0)
            PlayAnimatorGetHit(obj);
        else StartCoroutine(DestroyObj());


    }

    private IEnumerator DestroyObj()
    {
        isDie = true;

        gameObject.GetComponent<MeshCollider>().enabled = false;
        turtleAni.Play(die);
        yield return new WaitForSeconds(1.8f);

        Destroy(gameObject.transform.parent.gameObject);
    }

    void PlayAnimatorGetHit(GameObject obj)
    {
        turtleAni.Play(getHit);
    }

    public override void AttackAni()
    {
        if (isDie) return;
        turtleAni.Play(attack2);
    }

    public override void MoveAni()
    {
        if (isDie) return;
        turtleAni.Play(walk);
    }

    public override float SightRange()
    {
        return 30;
    }

    public override float AttackRange()
    {
        return 2;
    }
}