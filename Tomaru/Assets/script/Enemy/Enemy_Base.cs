using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [Header("Basic Stats")]
    public float moveSpeed = 2f;
    public int maxHP = 3;
    public int contactDamage = 1;

    protected int currentHP;
    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Player not found.");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected Vector2 GetDirectionToPlayer()
    {
        if (player == null) return Vector2.zero;
        return ((Vector2)(player.position - transform.position)).normalized;
    }
}