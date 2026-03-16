using UnityEngine;

public class Enemy_ChaseStop : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Detection / Stop Range")]
    public float stopRange = 5f;

    [Header("Target")]
    public Transform player;

    private Rigidbody2D rb;

    // 給其他 script（例如 Enemy_Shoot）讀取
    public bool IsPlayerInRange { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
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
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            IsPlayerInRange = false;
            return;
        }

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

            Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}