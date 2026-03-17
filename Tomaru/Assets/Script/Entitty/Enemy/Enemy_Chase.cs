using UnityEngine;

public class Enemy_Chase : Enemy_Base
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Chase player
    void FixedUpdate()
    {
        if (isKnockedBack) return;
        // calculate direction + move
        rb.linearVelocity = GetDirectionToPlayer() * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Entity player = collision.collider.GetComponent<Entity>();
        // Debug Log
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            player.TakeDamage(contactDamage,transform);
        }
    }
}