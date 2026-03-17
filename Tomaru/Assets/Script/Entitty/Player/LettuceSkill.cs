using UnityEngine;

public class LettuceSkill : MonoBehaviour
{
    public float knockbackForce = 100f;
    public float knockbackDuration = 1f;

    protected void ApplyKnockback(Enemy_Base enemy)
    {
        Vector2 direction = ((Vector2)enemy.transform.position - (Vector2)transform.position).normalized;
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * knockbackForce;
        Debug.Log(gameObject.name + " is knocked back.");
        StopCoroutine(nameof(KnockbackRoutine));
        StartCoroutine(KnockbackRoutine(enemy));
    }

    private System.Collections.IEnumerator KnockbackRoutine(Enemy_Base enemy)
    {
        enemy.isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        enemy.isKnockedBack = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Entity player = collision.GetComponent<Entity>();
        if (player != null)
        {
            Debug.Log("Lettuce Skill Picked");

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 60, 6);

            foreach (Collider2D hit in hits)
            {
                Enemy_Base enemy = hit.GetComponent<Enemy_Base>();

                if (enemy != null)
                {

                    ApplyKnockback(enemy);
                }
            }
            Destroy(gameObject);
        }
        
    }
}
