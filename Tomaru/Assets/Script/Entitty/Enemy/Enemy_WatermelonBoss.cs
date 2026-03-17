using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_WatermelonBoss : Enemy_Base
{
    [Header("Phase 2")]
    public GameObject slicePrefab;
    public int sliceCount = 5;

    bool isPhase2 = false;
    List<Enemy_WatermelonSlice> activeSlices = new List<Enemy_WatermelonSlice>();

    protected override void Awake()
    {
        base.Awake();
    }

    // DebugFˆÂ T ‹­§æ\á¢ Phase 2
    void Update()
    {
        // DebugFˆÂ T ‹­§æ\á¢ Phase 2
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHP = maxHP / 2;
            TakeDamage(0, transform);
        }
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        currentHP -= damage;
        ApplyKnockback(attacker);

        // Phase 2 æ\á¢ŠŒ
        if (!isPhase2 && currentHP <= maxHP / 2)
        {
            EnterPhase2();
            return;
        }

        if (currentHP <= 0)
            StartCoroutine(DeathRoutine());
    }

    void EnterPhase2()
    {
        isPhase2 = true;

        Vector2 bossPos = (Vector2)transform.position;

        // ¶¬¬¼‰Z
        float angleStep = 360f / sliceCount;

        for (int i = 0; i < sliceCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            Vector2 spawnPos = bossPos + offset;

            GameObject obj = Instantiate(slicePrefab, spawnPos, Quaternion.identity);
            Enemy_WatermelonSlice slice = obj.GetComponent<Enemy_WatermelonSlice>();

            slice.Init(this, currentHP / sliceCount);
            activeSlices.Add(slice);
        }

        // ‘å¼‰ZÁ¸
        Destroy(gameObject);
    }

    // ¬¼‰Z‰ñ™BŠQ
    public void TakeDamageFromSlice(int damage, Transform attacker)
    {
        if (isDead) return;

        currentHP -= damage;

        if (currentHP <= 0)
            BossDie();
    }

    // ¬¼‰Z€–S’Ê’m
    public void OnSliceDied(Enemy_WatermelonSlice slice)
    {
        activeSlices.Remove(slice);

        if (activeSlices.Count == 0)
            BossDie();
    }

    void BossDie()
    {
        foreach (Enemy_WatermelonSlice slice in activeSlices)
        {
            Destroy(slice.gameObject);
        }

        Destroy(gameObject);
    }
}