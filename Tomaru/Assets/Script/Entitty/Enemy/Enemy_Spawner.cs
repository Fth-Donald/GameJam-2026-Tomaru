using System.Collections;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Range")]
    public float minSpawnRadius = 8f;
    public float maxSpawnRadius = 12f;

    [Header("Optional")]
    public Transform player;

    void Awake()
    {
        // �@�� Inspector ���L�蓮�w�� player�C�A�����Q Player Tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("EnemySpawner: Player not found.");
            }
        }
    }

    // �����d��G�l
    public GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("EnemySpawner: enemyPrefab is null.");
            return null;
        }

        if (player == null)
        {
            Debug.LogWarning("EnemySpawner: player is null.");
            return null;
        }

        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        return spawnedEnemy;
    }

    // �A㔐������ǓG�l�i�� Wave �n���ċ��j
    public IEnumerator SpawnEnemies(GameObject[] enemyPrefabs, int spawnCount, float spawnInterval)
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: enemyPrefabs is empty.");
            yield break;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject selectedEnemy = enemyPrefabs[randomIndex];

            SpawnEnemy(selectedEnemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // �擾�߉ƚ����O�I笋@�����ʒu
    public Vector2 GetRandomSpawnPosition()
    {
        if (player == null)
        {
            Debug.LogWarning("EnemySpawner: player is null.");
            return Vector2.zero;
        }

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // ��訙|���C��Ƌɏ�����
        if (randomDirection == Vector2.zero)
        {
            randomDirection = Vector2.right;
        }

        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

        return (Vector2)player.position + randomDirection * randomDistance;
    }
}