using UnityEngine;

public class TilingBackground : MonoBehaviour
{
    [Header("Tile Size")]
    public float tileWidth = 12.8f;
    public float tileHeight = 7.2f;

    Transform cam;
    Vector3 lastCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
        // 初始位置對齊最近的 Tile 中心
        transform.position = new Vector3(
            Mathf.Round(cam.position.x / tileWidth) * tileWidth,
            Mathf.Round(cam.position.y / tileHeight) * tileHeight,
            transform.position.z
        );
    }

    void LateUpdate()
    {
        // Camera 移動了多少
        Vector3 delta = cam.position - lastCamPos;
        lastCamPos = cam.position;

        transform.position += delta;

        // 超出一個 Tile 寬度就跳回來
        float offsetX = transform.position.x - cam.position.x;
        float offsetY = transform.position.y - cam.position.y;

        if (Mathf.Abs(offsetX) >= tileWidth)
            transform.position -= new Vector3(Mathf.Sign(offsetX) * tileWidth, 0, 0);

        if (Mathf.Abs(offsetY) >= tileHeight)
            transform.position -= new Vector3(0, Mathf.Sign(offsetY) * tileHeight, 0);
    }
}