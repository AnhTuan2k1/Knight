

using System;
using System.Collections;
using UnityEngine;

public class Turtle : Enemy
{

    [SerializeField] private Animator turtleAni;
    private const float maxHealth = 1000;
    private static string getHit = "GetHit";
    //private static string attack1 = "Attack01";
    private static string attack2 = "Attack02";
    private static string walk = "WalkFWD";
    private static string die = "Die";
    bool isDie = false;
    float timeDie = 1.8f;
    private IBattleObserve battleSystem;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    //public override void Healing(float healthPoint)
    //{
    //    currentHealth += healthPoint;
    //    if (currentHealth > maxHealth) currentHealth = maxHealth;
    //    UpdateHealth(currentHealth / maxHealth);
    //}

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

        gameObject.GetComponentInChildren<MeshCollider>().enabled = false;
        turtleAni.Play(die);

        Destroy(gameObject, timeDie);
        Notify();
    }

    void PlayAnimatorGetHit(GameObject obj)
    {
        turtleAni.Play(getHit);
    }

    public override void Attack(int attackLevel = 1)
    {
        if (isDie) return;
        turtleAni.Play(attack2);
    }

    public override void Move()
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
        if (battleSystem != null) battleSystem.Update(this);
    }
}