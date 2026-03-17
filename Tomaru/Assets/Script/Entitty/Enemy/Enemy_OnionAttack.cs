using UnityEngine;
using System.Collections;

public class Enemy_OnionAttack : MonoBehaviour
{
    [Header("Attack")]
    public GameObject tearPrefab;          // 眼淚 Prefab
    public float attackInterval = 2f;      // 攻擊頻率
    public int spawnCount = 5;             // 每次生成幾個眼淚

    [Header("Phase 1 - Full Map")]
    public float mapRadius = 20f;          // 全地圖隨機範圍半徑
    public float phase1MinDelay = 0.1f;    // Phase 1 每個眼淚最短時間差
    public float phase1MaxDelay = 0.4f;    // Phase 1 每個眼淚最長時間差

    [Header("Phase 2 - Near Player")]
    public float nearPlayerRadius = 4f;    // 玩家附近隨機範圍半徑
    public float phase2MinDelay = 0f;      // Phase 2 每個眼淚最短時間差
    public float phase2MaxDelay = 0.8f;    // Phase 2 每個眼淚最長時間差

    [Header("Spawn Height")]
    public float spawnHeightOffset = 15f;  // 生成位置在落點上方多遠（Camera 外）

    Enemy_OnionBoss boss;
    Transform player;
    float attackTimer;

    void Awake()
    {
        boss = GetComponent<Enemy_OnionBoss>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        if (boss.IsPhase2)
        {
            // Phase 2：玩家附近，隨機時間差
            StartCoroutine(SpawnTearsWithDelay(false));
        }
        else
        {
            // Phase 1：全地圖隨機，也有時間差
            StartCoroutine(SpawnTearsWithDelay(true));
        }
    }

    IEnumerator SpawnTearsWithDelay(bool isPhase1)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (isPhase1)
            {
                SpawnTear(GetRandomMapPosition());
                float delay = Random.Range(phase1MinDelay, phase1MaxDelay);
                yield return new WaitForSeconds(delay);
            }
            else
            {
                SpawnTear(GetNearPlayerPosition());
                float delay = Random.Range(phase2MinDelay, phase2MaxDelay);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    void SpawnTear(Vector2 targetPos)
    {
        // 在落點正上方（Camera 外）生成眼淚
        Vector2 spawnPos = targetPos + Vector2.up * spawnHeightOffset;
        GameObject tear = Instantiate(tearPrefab, spawnPos, Quaternion.identity);

        // 傳入落點給眼淚
        tear.GetComponent<Enemy_OnionTear>().Init(targetPos);
    }

    Vector2 GetRandomMapPosition()
    {
        // Phase 1：全地圖隨機
        return (Vector2)transform.position + Random.insideUnitCircle * mapRadius;
    }

    Vector2 GetNearPlayerPosition()
    {
        // Phase 2：玩家附近隨機，讓玩家誤以為有瞄準傾向
        return (Vector2)player.position + Random.insideUnitCircle * nearPlayerRadius;
    }
}