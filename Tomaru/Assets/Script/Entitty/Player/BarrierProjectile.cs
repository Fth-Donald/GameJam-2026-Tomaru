using UnityEngine;

public class BarrierProjectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        int damage = 1;
        Transform transform = other.transform;
        if (!other.CompareTag("Enemy")) return;

        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage(damage, transform);
        }

        Destroy(gameObject);
    }

}