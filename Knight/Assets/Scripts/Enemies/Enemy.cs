
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
    public abstract void AttackAni();
    public abstract void MoveAni();
    public abstract void TakeDamage(float damage, GameObject obj);
    public abstract void Healing(float healthPoint);
    public void UpdateHealth(float fraction) => healthBar.fillAmount = fraction;
    public void VisibleAlert() => AlertBar.text = "!";
    public void InvisibleAlert() => AlertBar.text = null;
}
