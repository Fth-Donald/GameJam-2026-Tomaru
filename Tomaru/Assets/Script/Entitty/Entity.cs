using UnityEngine;
using UnityEngine.Rendering;

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
    public SfxPlayer sfx;
    private Coroutine knockbackCoroutine;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage, Transform attacker)
    {
        if (isDead || isInvincible) return;

        currentHealth -= damage;

        Debug.Log($"{gameObject.name} took {damage} damage from {(attacker != null ? attacker.name : "null")}");

        ApplyKnockback(attacker);

        if (currentHealth <= 0)
        {
            Die();
            sfx.PlayRandom(0, 6);
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

        Vector2 direction = ((Vector2)transform.position - (Vector2)attacker.position).normalized;

        Debug.Log($"{gameObject.name} knockback direction: {direction}");

        if (knockbackCoroutine != null)
        {
            StopCoroutine(knockbackCoroutine);
        }

        knockbackCoroutine = StartCoroutine(KnockbackRoutine(direction));
    }

    //protected virtual void FixedUpdate()
    //{
    //}

    private System.Collections.IEnumerator KnockbackRoutine(Vector2 direction)
    {
        isKnockedBack = true;

        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = direction * knockbackForce;

        Debug.Log(gameObject.name + " is knocked back.");

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
        knockbackCoroutine = null;
    }

}