using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_OnionPuddle : MonoBehaviour
{
    [Header("Puddle")]
    public int damage = 1;                 // 每次扣血量
    public float damageInterval = 0.5f;   // 每隔多久扣一次血
    public float lifetime = 5f;           // 水灘存在時間（眼淚產生的水灘在Inspector調短）

    [Header("Max Puddles")]
    public static int maxPuddles = 200;    // 全場最多水灘數量，超過就消滅最早生成的
    static Queue<Enemy_OnionPuddle> activePuddles = new Queue<Enemy_OnionPuddle>();

    float damageTimer;

    void Awake()
    {
        // 超過上限就消滅最早的水灘
        if (activePuddles.Count >= maxPuddles)
        {
            Enemy_OnionPuddle oldest = activePuddles.Dequeue();
            if (oldest != null)
                Destroy(oldest.gameObject);
        }

        activePuddles.Enqueue(this);
        StartCoroutine(LifetimeRoutine());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // 玩家在水灘範圍內持續扣血
        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            Entity entity = other.GetComponent<Entity>();
            entity.TakeDamage(damage, transform);
            damageTimer = 0f;
        }
    }

    IEnumerator LifetimeRoutine()
    {
        // 存在一段時間後自動消失
        yield return new WaitForSeconds(lifetime);
        activePuddles.TryDequeue(out _);
        Destroy(gameObject);
    }
}