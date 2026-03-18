using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    [Header("Stats")]
    public int damage = 1;          // 造成傷害量
    public float lifetime = 3f;     // 子彈自動消失時間
    public float hitDelay = 0.3f;   // 碰撞後延遲消失時間

    bool isHit = false;             // 防止重複觸發

    void Awake()
    {
        // 一段時間後自動消失
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Entity entity = collision.gameObject.GetComponent<Entity>();
            entity.TakeDamage(damage, transform);
            isHit = true;
            StartCoroutine(DestroyAfterDelay());
        }

        if (collision.gameObject.CompareTag("Barrier"))
        {
            isHit = true;
            StartCoroutine(DestroyAfterDelay());
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        // 讓玩家看到物理反彈效果
        yield return new WaitForSeconds(hitDelay);
        Destroy(gameObject);
    }
}