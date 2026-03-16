using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float shootInterval = 1.5f;
    public float bulletSpeed = 6f;

    private Enemy_ChaseStop chaseStop;
    private Transform player;

    float shootTimer;

    void Awake()
    {
        chaseStop = GetComponent<Enemy_ChaseStop>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null || bulletPrefab == null)
            return;

        if (!chaseStop.IsPlayerInRange)
            return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 direction =
            ((Vector2)player.position - (Vector2)transform.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}