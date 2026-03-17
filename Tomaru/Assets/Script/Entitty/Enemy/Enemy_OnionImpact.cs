using UnityEngine;
using System.Collections;

public class Enemy_OnionImpact : MonoBehaviour
{
    [Header("Impact")]
    public int damage = 1;                 // 造成傷害量
    public float lifetime = 0.2f;         // 攻擊判定存在時間（很短）

    void Awake()
    {
        // 短暫存在後消失，不留水灘
        StartCoroutine(ImpactRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 落地瞬間對玩家造成傷害
        Entity entity = other.GetComponent<Entity>();
        entity.TakeDamage(damage, transform);
    }

    IEnumerator ImpactRoutine()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}