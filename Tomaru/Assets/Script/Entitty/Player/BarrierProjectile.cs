using UnityEngine;

public class BarrierProjectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;
    public float damage = 1f;

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


    private void OnTriggerEnter2D(Collider2D obj)
    {

        if (!obj.CompareTag("Enemy")) return;
        Entity entity = obj.GetComponent<Entity>();
        if (entity != null)
        {
            entity.TakeDamage((int)damage, transform);
        }

        Destroy(gameObject);
    }

}