using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 2.5f;

    [Header("Knockback")]
    public float returnSpeed = 6f;
    public float damage = 1f;

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

        if (targetExtraDistance > 0f)
        {
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
    }   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        // Debug Log
        Entity enemy = collision.gameObject.GetComponent<Entity>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)damage, player.transform);
        }
    }
}
