
using UnityEngine.UI;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Image healthBar;

    public abstract void TakeDamage(float damage);
    public abstract void Healing(float healthPoint);
    public void UpdateHealth(float fraction) => healthBar.fillAmount = fraction;
}
