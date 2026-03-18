using UnityEngine;

public class BulletRotation : MonoBehaviour
{
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // š¤•Ğ‘fŞ’©ãCªŸˆÚ“®•ûŒüŒvZùçzŠp“x
        float angle = Mathf.Atan2(rb.linearVelocity.x, rb.linearVelocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }
}