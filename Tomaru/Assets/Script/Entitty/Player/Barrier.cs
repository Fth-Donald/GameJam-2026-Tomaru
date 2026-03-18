using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BarrierOrbit : MonoBehaviour
{
    public Transform player;
    PlayerController pc = null;
    public float orbitRadius = 2.5f;

    [Header("Knockback / Punch")]
    public float punchDistance = 1.5f;
    public float punchSpeed = 10f;
    public float returnSpeed = 6f;
    public float damage = 1f;

    [Header("Flying Attack")]
    public GameObject flyingBarrierPrefab;
    public float shootCooldown =  1.5f;

    private Camera mainCam;
    private float currentExtraDistance = 0f;
    private float targetExtraDistance = 0f;
    private Vector2 lastDirection = Vector2.right;
    private float shootTimer = 1.5f;
    public float dshootInterval = 0.03f;

    private void Start()
    {
        pc = this.GetComponentInParent<PlayerController>();
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
            if (pc.dshoot && pc.sshoot)
            {
                ShootFlyingBarrier(2, 2);
            }
            else if (pc.dshoot)
            {
                ShootFlyingBarrier(1,2);
            }
            else if (pc.sshoot)
            {
                ShootFlyingBarrier(2,1);
            }
            else return;
        }
    }

    private void ShootFlyingBarrier(int x, int y)
    {
        shootTimer = shootCooldown;
        if (flyingBarrierPrefab == null) return;

        if (x == 1)
        {
            SpawnProjectile(lastDirection);
            if(y== 2)
            {
                StartCoroutine(DoubleShotRoutine(1));
            }
        }
        else if (x == 2)
        {
            float splitAngle = 30f; // angle between center and side projectiles
            SpawnProjectile(lastDirection);
            SpawnProjectile(Quaternion.Euler(0, 0, splitAngle) * lastDirection);
            SpawnProjectile(Quaternion.Euler(0, 0, -splitAngle) * lastDirection);
            if (y == 2)
            {
                StartCoroutine(DoubleShotRoutine(2));
            }
        }
    }

    private System.Collections.IEnumerator DoubleShotRoutine(int x)
    {
        yield return new WaitForSeconds(dshootInterval);
        if (x == 1)
        {
            SpawnProjectile(lastDirection);
        }
        else if (x == 2)
        {
            float splitAngle = 30f; // angle between center and side projectiles
            SpawnProjectile(lastDirection);
            SpawnProjectile(Quaternion.Euler(0, 0, splitAngle) * lastDirection);
            SpawnProjectile(Quaternion.Euler(0, 0, -splitAngle) * lastDirection);
        }
    }

    private void SpawnProjectile(Vector2 direction)
    {
        GameObject spawnedProjectile = Instantiate(
            flyingBarrierPrefab,
            transform.position,
            Quaternion.identity
        );

        BarrierProjectile projectile = spawnedProjectile.GetComponent<BarrierProjectile>();

        if (projectile != null)
        {
            projectile.Initialize(direction.normalized);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Enemy_Base enemy = collision.collider.gameObject.GetComponent<Enemy_Base>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)damage, transform); // 用護盾自己的 transform，不用 player
        }
    }
}