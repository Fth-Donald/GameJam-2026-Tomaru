using UnityEngine;

public class TilingBackground : MonoBehaviour
{
    [Header("Tile Size")]
    public float tileWidth = 12.8f; // 圖片世界座標寬度（1280 / 100）
    public float tileHeight = 7.2f; // 圖片世界座標寬度（720 / 100）
    Transform Cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Cam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 根據 Camera 位置計算最近的 Tile 中心點
        float camX = Cam.position.x;
        float camY = Cam.position.y;

        float offsetX = Mathf.Round(camX / tileWidth) * tileWidth;
        float offsetY = Mathf.Round(camY / tileHeight) * tileHeight;
        
        // 背景跟著 Camera 的 Tile 中心點移動
        transform.position=new Vector3(offsetX,offsetY,transform.position.z);
    }
}
