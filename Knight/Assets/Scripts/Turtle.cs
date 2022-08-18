

public class Turtle : Character
{
    private const float maxHealth = 1000;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void Healing(float healthPoint)
    {
        currentHealth += healthPoint;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealth(currentHealth / maxHealth);
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage; 
        UpdateHealth(currentHealth / maxHealth);

        print(currentHealth);
    }


}