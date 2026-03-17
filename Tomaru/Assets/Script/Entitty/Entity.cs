using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public int maxHealth = 5;
    public int currentHealth;
    public float invincibleTime = 1f;
    public float knockbackForce = 6f;
    public float deathDelay = 0.3f;

    [Header("Current Status")]
    public bool isDead = false;
    
    private bool isInvincible = false;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    //Entity takes damage
    public virtual void TakeDamage(int damage,Transform attacker)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        ApplyKnockback(attacker);

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

    //This controls the invisibility timer of the entity
    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    //Can be used for heal item and vamp
    public virtual void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += (int)amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    //Death
    protected virtual void Die()
    {
        if (isDead) return;
        Debug.Log(gameObject.name + " died.");
        isDead = true;
        Destroy(gameObject);
    }

    //KnockBack
    protected void ApplyKnockback(Transform attacker)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)attacker.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

}