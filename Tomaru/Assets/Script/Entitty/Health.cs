using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 5;
    public float invincibleTime = 1f;
    public bool isDead = false;

    private int currentHealth;
    private bool isInvincible = false;
    

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public virtual void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += (int)amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        if(isDead) return;
        Debug.Log(gameObject.name + " died.");
        isDead = true;
        //Destroy(gameObject);
    }
}