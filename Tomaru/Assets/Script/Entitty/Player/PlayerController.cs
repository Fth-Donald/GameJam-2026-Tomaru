using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Entity
{   
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        if (isKnockedBack) return;
        rb.linearVelocity = moveInput * moveSpeed;
    }

    protected override void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Game Over");
        gameObject.SetActive(false);
    }

    
}