using UnityEngine;

public class TilingBackground : MonoBehaviour
{
    Transform Cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // ”wŒiæîZCamera‘–CTile–Í®©“®•İŸŞ,”wŒiæî’˜ Camera “I Tile ’†SêyˆÚ“®
        transform.position=new Vector3(
        Cam.position.x,
        Cam.position.y,
        transform.position.z
        );
    }
}
