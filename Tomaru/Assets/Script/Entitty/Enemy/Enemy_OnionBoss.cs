using UnityEngine;
using System.Collections;

public class Enemy_OnionBoss : Enemy_Base
{
    [Header("Phase 2 Movement")]
    public float phase2MoveSpeed = 1.5f;
    public float moveDistance = 3f;         // 每段移動距離

    [Header("Puddle")]
    public GameObject puddlePrefab;
    public float puddleSpawnInterval = 0.5f; // 每隔多遠生成一個水灘

    [Header("Camera Boundary")]
    public float boundaryPadding = 0.5f;    // 距離邊界的緩衝距離

    [Header("Barrier Collision")]
    public float knockbackStopDuration = 0.2f; // 被護盾撞到後停下的時間

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

    bool isKnockedBack = false;          // 被護盾撞到後的停止狀態

    Vector2 moveDirection;
    float distanceTravelled = 0f;
    float puddleDistanceTracker = 0f;
    Vector2 lastPosition;

    Camera mainCam;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPhase2) return;
        if (!collision.gameObject.CompareTag("Barrier")) return;

        // 碰到護盾：停下並換方向
        StartCoroutine(BarrierKnockbackRoutine());
    }

    IEnumerator BarrierKnockbackRoutine()
    {
        // 停止移動
        isKnockedBack = true;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(knockbackStopDuration);

        // 恢復移動，馬上換方向
        isKnockedBack = false;
        PickNewDirection();
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

    Bounds GetCameraBounds()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;
        Vector2 camPos = mainCam.transform.position;
        return new Bounds(camPos, new Vector3(camWidth * 2, camHeight * 2, 0));
    }

    void FixedUpdate()
    {
        if (!isPhase2) return;
        if (isKnockedBack) return;

        // 移動
        rb.linearVelocity = moveDirection * phase2MoveSpeed;

        // 取得 Camera 邊界
        Bounds bounds = GetCameraBounds();
        float minX = bounds.min.x + boundaryPadding;
        float maxX = bounds.max.x - boundaryPadding;
        float minY = bounds.min.y + boundaryPadding;
        float maxY = bounds.max.y - boundaryPadding;

        // 超出邊界時夾回邊界內並換方向
        Vector2 pos = transform.position;
        if (pos.x <= minX || pos.x >= maxX || pos.y <= minY || pos.y >= maxY)
        {
            transform.position = new Vector2(
                Mathf.Clamp(pos.x, minX, maxX),
                Mathf.Clamp(pos.y, minY, maxY)
            );
            PickNewDirection();
        }

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