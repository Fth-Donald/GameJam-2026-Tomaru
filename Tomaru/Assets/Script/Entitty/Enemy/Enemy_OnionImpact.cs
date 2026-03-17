using UnityEngine;
using System.Collections;

public class Enemy_OnionImpact : MonoBehaviour
{
    [Header("Impact")]
    public int damage = 1;                 // 造成傷害量
    public float lifetime = 0.2f;         // 攻擊判定存在時間（很短）

    [Header("Prefabs")]
    public GameObject puddlePrefab;        // 消失後生成的水灘

    void Awake()
    {
        // 短暫存在後消失，並生成水灘
        StartCoroutine(ImpactRoutine());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 對玩家造成傷害
        Entity entity = other.GetComponent<Entity>();
        entity.TakeDamage(damage, transform);
    }

    IEnumerator ImpactRoutine()
    {
        yield return new WaitForSeconds(lifetime);

        // 在同一位置生成水灘
        Instantiate(puddlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}