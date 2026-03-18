using UnityEngine;

public class BarrierOrbit : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 2.5f;

    [Header("Knockback / Punch")]
    public float punchDistance = 1.5f;
    public float punchSpeed = 10f;
    public float returnSpeed = 6f;
    public float damage = 1f;

    [Header("Flying Attack")]
    public GameObject flyingBarrierPrefab;
    public float shootCooldown = 0.25f;

    private Camera mainCam;
    private float currentExtraDistance = 0f;
    private float targetExtraDistance = 0f;
    private Vector2 lastDirection = Vector2.right;
    private float shootTimer = 0f;

    private void Start()
    {
        mainCam = Camera.main;

        if (player == null)
        {
            player = transform.parent;
        }
    }

    private void Update()
    {
        if (player == null || mainCam == null) return;

        shootTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            targetExtraDistance = punchDistance;
        }

        if (targetExtraDistance > 0f)
        {
            currentExtraDistance = Mathf.MoveTowards(
                currentExtraDistance,
                targetExtraDistance,
                punchSpeed * Time.deltaTime
            );

            if (Mathf.Approximately(currentExtraDistance, targetExtraDistance))
            {
                targetExtraDistance = 0f;
            }
        }
        else
        {
            currentExtraDistance = Mathf.MoveTowards(
                currentExtraDistance,
                0f,
                returnSpeed * Time.deltaTime
            );
        }

        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCam.transform.position.z);

        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector2 rawDirection = mouseWorldPos - player.position;

        if (rawDirection.sqrMagnitude > 0.001f)
        {
            lastDirection = rawDirection.normalized;
        }

        float finalRadius = orbitRadius + currentExtraDistance;
        Vector3 targetPosition = player.position + (Vector3)(lastDirection * finalRadius);

        transform.position = targetPosition;

        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(1) && shootTimer <= 0f)
        {
            ShootFlyingBarrier();
            shootTimer = shootCooldown;
        }
    }

    private void ShootFlyingBarrier()
    {
        if (flyingBarrierPrefab == null) return;

        GameObject spawnedProjectile = Instantiate(
            flyingBarrierPrefab,
            transform.position,
            Quaternion.identity
        );

        BarrierProjectile projectile = spawnedProjectile.GetComponent<BarrierProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(lastDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        player = transform.parent;
        if (!collision.gameObject.CompareTag("Enemy")) return;
        // Debug Log
        Enemy_Base enemy = collision.collider.gameObject.GetComponent<Enemy_Base>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)damage, player.transform);
        }
    }
}