using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 1;          // 造成傷害量
    public float lifetime = 3f;     // 子彈自動消失時間

    void Awake()
    {
        // 一段時間後自動消失
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 碰到 Player 造成傷害
        if (other.CompareTag("Player"))
        {
            Entity entity = other.GetComponent<Entity>();
            entity.TakeDamage(damage, transform);
            Destroy(gameObject);
        }

        // 碰到 Barrier 消失
        if (other.CompareTag("Barrier"))
        {
            Destroy(gameObject);
        }
    }
}
