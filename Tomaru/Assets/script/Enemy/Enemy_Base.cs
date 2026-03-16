using UnityEngine;
using System.Collections;

public class Enemy_Base : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public int maxHP = 3;
    public int contactDamage = 1;

    [Header("Knockback")]
    public float knockbackForce = 6f;

    [Header("Death")]
    public float deathDelay = 0.3f;

    protected int currentHP;
    protected bool isDead = false;
    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        // find player
        player = GameObject.FindWithTag("Player").transform;
    }

    public virtual void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        currentHP -= damage;
        ApplyKnockback(attacker);

        if (currentHP <= 0)
            StartCoroutine(DeathRoutine());
    }

    protected void ApplyKnockback(Transform attacker)
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)attacker.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    protected virtual IEnumerator DeathRoutine()
    {
        isDead = true;
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }

    protected Vector2 GetDirectionToPlayer()
    {
        return ((Vector2)(player.position - transform.position)).normalized;
    }
}