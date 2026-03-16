using UnityEngine;

public class Enemy_Orbit : MonoBehaviour
{
    [Header("Orbit")]
    public float orbitRadius = 1.5f;  // 公轉半徑
    public float orbitSpeed = 180f;   // 每秒轉幾度

    private float currentAngle = 0f;

    void Update()
    {
        // 累積角度
        currentAngle += orbitSpeed * Time.deltaTime;

        // 根據角度計算圍繞 Pivot 的位置
        float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * orbitRadius;
        float y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * orbitRadius;

        // 設定相對於 Pivot（parent）的 local position
        transform.localPosition = new Vector2(x, y);
    }
}