using UnityEngine;

public class Enemy_Chase : Enemy_Base
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Chase player
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // calculate direction + move
        rb.linearVelocity = GetDirectionToPlayer() * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug Log
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }
}
