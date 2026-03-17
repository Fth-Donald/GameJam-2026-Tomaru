using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Entity
{
    [SerializeField] GameObject barrier;
    [SerializeField] GameObject sword;
    private bool usingBarrier = true;

    public Image healthBar;

    [Header("Movement")]
    

    
    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        EquipBarrier();
    }

    private void Update()
    {
        if (isKnockedBack) return;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWeapon();
        }

        //healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void FixedUpdate()
    {
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

    private void ToggleWeapon()
    {
        usingBarrier = !usingBarrier;

        if (usingBarrier)
        {
            EquipBarrier();
        }
        else
        {
            EquipSword();
        }
    }

    private void EquipBarrier()
    {
        barrier.SetActive(true);
        sword.SetActive(false);
    }

    private void EquipSword()
    {
        barrier.SetActive(false);
        sword.SetActive(true);
    }

}