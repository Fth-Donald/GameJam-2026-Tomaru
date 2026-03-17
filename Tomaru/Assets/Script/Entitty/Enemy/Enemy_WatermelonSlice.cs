using UnityEngine;
using System.Collections;

public class Enemy_WatermelonSlice : Enemy_Base
{
    Enemy_WatermelonBoss boss;

    protected override void Awake()
    {
        base.Awake();
    }

    // 由 Boss 初始化
    public void Init(Enemy_WatermelonBoss bossRef, int initHP)
    {
        boss = bossRef;
        currentHP = initHP;
    }

    public override void TakeDamage(int damage, Transform attacker)
    {
        if (isDead) return;

        // 傷害回傳給 Boss
        boss.TakeDamageFromSlice(damage, attacker);

        // 自己 HP 同步
        currentHP -= damage;

        if (currentHP <= 0)
            StartCoroutine(DeathRoutine());
    }

    protected override IEnumerator DeathRoutine()
    {
        isDead = true;

        // 通知 Boss 自己死了
        boss.OnSliceDied(this);

        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
    }
}