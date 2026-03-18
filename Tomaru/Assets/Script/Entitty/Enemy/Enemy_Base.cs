using UnityEngine;
using System.Collections;

public class Enemy_Base : Entity
{
    [Header("Stats")]
    public int contactDamage = 1;
    protected Transform player;

    [Header("Drops")]
    public GameObject[] dropPrefabs;

    [Range(0f, 1f)]
    public float dropChance = 0.25f; // 25% chance

    public WaveScript wave;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").transform;
    }

    protected Vector2 GetDirectionToPlayer()
    {
        return ((Vector2)(player.position - transform.position)).normalized;
    }

    protected override void Die()
    {
        if (isDead) return;

        SpawnDrop();
        wave.OnEnemyKilled();

        base.Die();
    }

    public void SpawnDrop()
    {
        if (dropPrefabs == null || dropPrefabs.Length == 0) return;

        // roll whether to drop at all
        if (Random.value > dropChance) return;

        int index = Random.Range(0, dropPrefabs.Length);

        Instantiate(
            dropPrefabs[index],
            transform.position,
            Quaternion.identity
        );
    }
}