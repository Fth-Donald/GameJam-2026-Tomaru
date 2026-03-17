using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public int maxHealth = 5;
    public int currentHealth;
    public float invincibleTime = 1f;
    public float knockbackForce = 10f;
    public float deathDelay = 0.3f;
    public float knockbackDuration = 0.2f;

    [Header("Current Status")]
    public bool isDead = false;
    public bool isKnockedBack = false;
    public bool isInvincible = false;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage, Transform attacker)
    {
        if (isDead || isInvincible) return;

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

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    public virtual void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += (int)amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    protected virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject, deathDelay);
    }

    protected void ApplyKnockback(Transform attacker)
    {
        if (attacker == null || rb == null) return;

        isKnockedBack = true;

        Vector2 direction = ((Vector2)transform.position - (Vector2)attacker.position).normalized;

        rb.linearVelocity = direction * knockbackForce;
        Debug.Log(gameObject.name + " is knocked back.");

        StopCoroutine(nameof(KnockbackRoutine));
        StartCoroutine(KnockbackRoutine());
    }

    private System.Collections.IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
        rb.linearVelocity = Vector2.zero;
    }


}