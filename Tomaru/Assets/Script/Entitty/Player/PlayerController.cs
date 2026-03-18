using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Entity
{
    [SerializeField] GameObject barrier;
    [SerializeField] GameObject sword;
    private bool usingBarrier = true;

    public HPScript hPScript;

    //SKILLS
    int skill1 = 0;
    int skill2 = 0;

    [Header("BerrySkill")]
    public bool bspicked = false;
    //public bool btriggered = false;
    public float eTime = 10f;
    public float nSpeed = 100f;



    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        EquipBarrier();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        //Use Skill
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bspicked)
            {
                BerrySkill();
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleWeapon();
        }

        //healthBar.fillAmount = currentHealth / maxHealth;
    }

    protected void FixedUpdate()
    {
        if (isDead || isKnockedBack) return;
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

    private void BerrySkill()
    {
        bspicked = false;
        StartCoroutine(BerrySkillRoutine());
    }

    private System.Collections.IEnumerator BerrySkillRoutine()
    {
        float originalSpeed = moveSpeed;

        moveSpeed = nSpeed;

        yield return new WaitForSeconds(eTime);

        moveSpeed = originalSpeed;
    }

}