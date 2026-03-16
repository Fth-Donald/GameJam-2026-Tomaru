using UnityEngine;

public class Enemy_Shoot : Enemy_Base
{
    [Header("Shoot")]
    public GameObject bulletPrefab;
    public float shootInterval = 1.5f;
    public float bulletSpeed = 6f;

    private Enemy_ChaseStop chaseStop;
    private float shootTimer;

    protected override void Awake()
    {
        base.Awake();
        chaseStop = GetComponent<Enemy_ChaseStop>();
    }

    void Update()
    {
        if (!chaseStop.IsPlayerInRange) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 direction = GetDirectionToPlayer();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}