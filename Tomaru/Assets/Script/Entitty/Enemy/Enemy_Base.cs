using UnityEngine;
using System.Collections;

public class Enemy_Base : Entity
{
    [Header("Stats")]
    public int contactDamage = 1;
    protected Transform player;
    public GameObject[] dropPrefabs;
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

        wave.OnEnemyKilled();

        base.Die();


    }
    public void SpawnDrop()
    {
        if (dropPrefabs.Length == 0) return;

        int index = Random.Range(0, dropPrefabs.Length*4);

        Instantiate(
            dropPrefabs[index],
            transform.position,
            Quaternion.identity
        );
    }
}