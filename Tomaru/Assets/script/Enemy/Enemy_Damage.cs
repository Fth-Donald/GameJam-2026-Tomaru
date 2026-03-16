using UnityEngine;
using System.Collections;

public class Enemy_Damage : MonoBehaviour
{
    [Header("Health")]
    public int maxHP = 3;

    [Header("Knockback")]
    public float knockbackForce = 6f;

    [Header("Death")]
    public float deathDelay = 0.3f;

    int currentHP;
    bool isDead = false;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            TakeDamage(1, collision.transform);
        }
    }

    public void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        currentHP -= damage;

        ApplyKnockback(attacker);

        if (currentHP <= 0)
        {
            StartCoroutine(DeathRoutine());
        }
    }

    void ApplyKnockback(Transform attacker)
    {
        if (rb == null) return;

        Vector2 direction =
            ((Vector2)transform.position - (Vector2)attacker.position).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator DeathRoutine()
    {
        isDead = true;

        yield return new WaitForSeconds(deathDelay);

        Destroy(gameObject);
    }
}