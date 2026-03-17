using UnityEngine;

public class Enemy_OnionBoss : Enemy_Base
{
    [Header("Phase 2 Movement")]
    public float phase2MoveSpeed = 1.5f;
    public float moveDistance = 3f;         // 每段移動距離

    [Header("Puddle")]
    public GameObject puddlePrefab;
    public float puddleSpawnInterval = 0.5f; // 每隔多遠生成一個水灘

    // 45度方位（東北、東南、西南、西北）
    readonly Vector2[] phase2Directions = new Vector2[]
    {
        new Vector2(1, 1).normalized,    // 東北
        new Vector2(1, -1).normalized,   // 東南
        new Vector2(-1, -1).normalized,  // 西南
        new Vector2(-1, 1).normalized    // 西北
    };

    bool isPhase2 = false;
    public bool IsPhase2 => isPhase2;

    Vector2 moveDirection;
    float distanceTravelled = 0f;
    float puddleDistanceTracker = 0f;
    Vector2 lastPosition;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Phase 1 不被 knockback（不動如山）
        if (!isPhase2)
        {
            if (currentHealth <= maxHealth / 2)
                EnterPhase2();

            if (currentHealth <= 0)
                Die();

            return;
        }

        // Phase 2 正常 knockback
        ApplyKnockback(attacker);

        if (currentHealth <= 0)
            Die();
    }

    void EnterPhase2()
    {
        isPhase2 = true;
        lastPosition = transform.position;
        PickNewDirection();
    }

    void PickNewDirection()
    {
        int index = Random.Range(0, phase2Directions.Length);
        moveDirection = phase2Directions[index];
        distanceTravelled = 0f;
    }

    void FixedUpdate()
    {
        if (!isPhase2) return;

        // 移動
        rb.linearVelocity = moveDirection * phase2MoveSpeed;

        // 累積移動距離
        float frameDist = Vector2.Distance((Vector2)transform.position, lastPosition);
        distanceTravelled += frameDist;
        puddleDistanceTracker += frameDist;
        lastPosition = (Vector2)transform.position;

        // 水灘生成
        if (puddleDistanceTracker >= puddleSpawnInterval)
        {
            Instantiate(puddlePrefab, transform.position, Quaternion.identity);
            puddleDistanceTracker = 0f;
        }

        // 移動完一段距離後換方向
        if (distanceTravelled >= moveDistance)
            PickNewDirection();
    }
}