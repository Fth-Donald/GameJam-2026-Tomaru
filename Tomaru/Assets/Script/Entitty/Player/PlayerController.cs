using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Entity
{
    [SerializeField] GameObject barrier;
    [SerializeField] GameObject sword;
    private bool usingBarrier = true;
    public HPScript hPScript;

    //SKILLS
    public int skill1 = 0;
    public int skill2 = 0;


    [Header("BerrySkill")]
    //Berry skill is skill no 1
    public float fTime = 10f;
    public float fSpeed = 50f;



    [Header("BroccoliSkill")]
    //Broccoli skill is skill no 3
    public bool dshoot = false;
    public float shootTime = 10f;

    [Header("BananaSkill")]
    //Banana skill is skill no 4
    public bool sshoot = false;
    public float sTime = 10f;


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
            switch (skill1)
            {
                case 1:
                    BerrySkill(1);
                    break;
                case 2:
                    break;
                case 3:
                    BroccoliSkill(1);
                    break;
                case 4:
                    BananaSkill(1);
                    break;
                case 0:
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (skill2)
            {
                case 1:
                    BerrySkill(2);
                    break;
                case 2:
                    break;
                case 3:
                    BroccoliSkill(2);
                    break;
                case 4:
                    BananaSkill(2);
                    break;
                case 0:
                    break;
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


    //BERRY
    private void BerrySkill(int s)
    {
        if (s == 1)
        {
            skill1 = 0;
        }
        else if (s == 2)
        {
            skill2 = 0;
        }
        StartCoroutine(BerrySkillRoutine());
    }

    private System.Collections.IEnumerator BerrySkillRoutine()
    {
        float originalSpeed = moveSpeed;

        moveSpeed = fSpeed;

        yield return new WaitForSeconds(fTime);

        moveSpeed = originalSpeed;
    }


    //BROCCOLI
        private void BroccoliSkill(int s)
    {
        if (s == 1)
        {
            skill1 = 0;
        }
        else if (s == 2)
        {
            skill2 = 0;
        }
        StartCoroutine(BroccoliSkillRoutine());
    }

    private System.Collections.IEnumerator BroccoliSkillRoutine()
    {
        dshoot = true;

        yield return new WaitForSeconds(shootTime);

        dshoot = false;
    }


    //BANANA
    private void BananaSkill(int s)
    {
        if (s == 1)
        {
            skill1 = 0;
        }
        else if (s == 2)
        {
            skill2 = 0;
        }
        StartCoroutine(BananaSkillRoutine());
    }

    private System.Collections.IEnumerator BananaSkillRoutine()
    {
        sshoot = true;

        yield return new WaitForSeconds(shootTime);

        sshoot = false;
    }

}