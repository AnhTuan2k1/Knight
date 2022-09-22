
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected TextMeshProUGUI AlertBar;

    public virtual float SightRange() => 30;
    public virtual float AttackRange() => 2.5f;
    /// <summary>
    /// TimeBetweenAttacks should be at least 2 seconds
    /// </summary>
    /// <returns></returns>
    public virtual float TimeBetweenAttacks() => 5;
    public abstract void Attack(int attackLevel = 1);
    public abstract void Move();
    public abstract void TakeDamage(float damage, GameObject obj);
    public void UpdateHealth(float fraction) => healthBar.fillAmount = fraction;
    public void VisibleAlert() => AlertBar.text = "!";
    public void InvisibleAlert() => AlertBar.text = null;
    public virtual float realY() => 0;

    public abstract void Attach(IBattleObserve battleObserve);
    public abstract void Detach(IBattleObserve battleObserve);
    public abstract void Notify();
}
