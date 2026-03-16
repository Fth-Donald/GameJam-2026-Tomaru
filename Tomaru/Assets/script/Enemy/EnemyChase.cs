using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float moveSpeed = 2.0f;
    Transform player;
    Rigidbody2D rb;
    
     // Chase player
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //find player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;

        // calculate direction
        Vector2 direction = (player.position - transform.position).normalized;

        // move
        rb.linearVelocity = direction * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug Log
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
        }
    }
}