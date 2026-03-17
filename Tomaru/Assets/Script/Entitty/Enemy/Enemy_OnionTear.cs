using UnityEngine;

public class Enemy_OnionTear : MonoBehaviour
{
    [Header("Movement")]
    public float fallSpeed = 8f;           // 落下速度

    [Header("Prefabs")]
    public GameObject impactPrefab;        // 抵達落點後生成的攻擊判定

    // 目標落點，由 Enemy_OnionAttack 生成時指定
    Vector2 targetPosition;
    bool isMoving = false;

    // 由 Enemy_OnionAttack 呼叫，設定落點
    public void Init(Vector2 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        // 從生成位置移動到落點
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            fallSpeed * Time.deltaTime
        );

        // 抵達落點
        if ((Vector2)transform.position == targetPosition)
        {
            // 在落點生成攻擊判定
            Instantiate(impactPrefab, targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}