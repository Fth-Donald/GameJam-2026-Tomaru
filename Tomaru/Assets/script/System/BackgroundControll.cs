using UnityEngine;

public class BackgroundControll : MonoBehaviour
{
    private float startPos, length, high;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        high = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect;
        float distanceY = cam.transform.position.y * parallaxEffect;
        float movementX = cam.transform.position.x * (1 - parallaxEffect);
        float movementY = cam.transform.position.y * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distanceX, startPos + distanceY, transform.position.z);

        if (movementX > startPos + length)
        {
            startPos += length;
        }

        else if (movementX < startPos - length)
        {
            startPos -= length;
        }

        else if (movementY > startPos + high)
        {
            startPos += high;
        }

        else if (movementY < startPos - high)
        {
            startPos -= high;
        }
    }
}
