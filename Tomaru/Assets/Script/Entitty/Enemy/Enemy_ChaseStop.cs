using UnityEngine;

public class Enemy_ChaseStop : Enemy_Base
{
    [Header("Detection / Stop Range")]
    public float stopRange = 5f;

    // 給其他 script（例如 Enemy_Shoot）讀取
    public bool IsPlayerInRange { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopRange)
        {
            // 玩家進入範圍：停止
            IsPlayerInRange = true;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            // 玩家不在範圍內：追擊
            IsPlayerInRange = false;
            rb.linearVelocity = GetDirectionToPlayer() * moveSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}