using UnityEngine;
using System.Collections;

public class Enemy_WatermelonSlice : Enemy_Base
{
    [Header("Chase Stop")]
    public float stopRange = 5f;            // 停止追擊範圍

    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 6f;
    public float shootInterval = 1.5f;      // 射擊間隔

    [Header("Reposition")]
    public float minRepositionTime = 1f;    // 最短換位置時間
    public float maxRepositionTime = 4f;    // 最長換位置時間
    public float orbitRadius = 5f;          // 繞玩家的距離

    Enemy_WatermelonBoss boss;
    float shootTimer;
    float repositionTimer;
    bool isMovingToNewPos = false;
    Vector2 targetPos;

    protected override void Awake()
    {
        base.Awake();
        // 初始化換位置計時器
        repositionTimer = Random.Range(minRepositionTime, maxRepositionTime);
    }

    // 由 Boss 初始化
    public void Init(Enemy_WatermelonBoss bossRef, int initHP)
    {
        boss = bossRef;
        currentHealth = initHP;
    }

    void PickNewOrbitPosition()
    {
        // 在玩家周圍隨機選一個角度，保持 orbitRadius 距離
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius;
        targetPos = (Vector2)player.position + offset;
        isMovingToNewPos = true;

        // 重置換位置計時器
        repositionTimer = Random.Range(minRepositionTime, maxRepositionTime);
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (isMovingToNewPos)
        {
            // 移動到新位置
            rb.linearVelocity = ((Vector2)targetPos - (Vector2)transform.position).normalized * moveSpeed;

            // 抵達新位置
            if (Vector2.Distance(transform.position, targetPos) <= 0.3f)
            {
                isMovingToNewPos = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else if (distance <= stopRange)
        {
            // 在射擊範圍內停止
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            // 追擊玩家
            rb.linearVelocity = GetDirectionToPlayer() * moveSpeed;
        }
    }

    void Update()
    {
        // 射擊計時（任何狀態都繼續射擊）
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= stopRange)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }

        // 換位置計時
        repositionTimer -= Time.deltaTime;
        if (repositionTimer <= 0f)
            PickNewOrbitPosition();
    }

    void Shoot()
    {
        Vector2 direction = GetDirectionToPlayer();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        // 傷害回傳給 Boss
        boss.TakeDamageFromSlice(damage, attacker);

        // 自己 HP 同步
        currentHealth -= damage;

        if (currentHealth <= 0)
            StartCoroutine(DeathRoutine());
    }

    protected IEnumerator DeathRoutine()
    {
        isDead = true;

        // 通知 Boss 自己死了
        boss.OnSliceDied(this);

        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}