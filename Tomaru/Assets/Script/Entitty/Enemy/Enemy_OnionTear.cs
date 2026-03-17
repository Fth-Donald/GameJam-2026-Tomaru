using UnityEngine;

public class Enemy_OnionTear : MonoBehaviour
{
    [Header("Movement")]
    public float fallSpeed = 8f;            // 落下速度

    [Header("Prefabs")]
    public GameObject impactPrefab;         // 抵達落點後生成的攻擊判定
    public GameObject shadowPrefab;         // 落點陰影

    [Header("Shadow")]
    public float maxShadowScale = 1f;       // 陰影最大 Scale（眼淚剛生成時）
    public float minShadowScale = 0.1f;     // 陰影最小 Scale（眼淚抵達落點時）

    // 目標落點，由 Enemy_OnionAttack 生成時指定
    Vector2 targetPosition;
    bool isMoving = false;

    GameObject shadowInstance;              // 生成出來的陰影物件
    float initialDistance;                  // 眼淚剛生成時距落點的距離

    // 由 Enemy_OnionAttack 呼叫，設定落點
    public void Init(Vector2 target)
    {
        targetPosition = target;
        isMoving = true;

        // 記錄初始距離，用來計算陰影縮放比例
        initialDistance = Vector2.Distance(transform.position, targetPosition);

        // 在落點生成陰影
        shadowInstance = Instantiate(shadowPrefab, targetPosition, Quaternion.identity);

        // 陰影初始 Scale 設為最大
        shadowInstance.transform.localScale = Vector3.one * maxShadowScale;
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

        // 根據剩餘距離更新陰影 Scale
        // progress：0 = 剛生成，1 = 抵達落點
        float currentDistance = Vector2.Distance(transform.position, targetPosition);
        float progress = 1f - (currentDistance / initialDistance);

        // 距離越遠 Scale 越大，距離越近 Scale 越小
        float shadowScale = Mathf.Lerp(maxShadowScale, minShadowScale, progress);
        shadowInstance.transform.localScale = Vector3.one * shadowScale;

        // 抵達落點
        if ((Vector2)transform.position == targetPosition)
        {
            // 消滅陰影
            Destroy(shadowInstance);

            // 在落點生成攻擊判定
            Instantiate(impactPrefab, targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}