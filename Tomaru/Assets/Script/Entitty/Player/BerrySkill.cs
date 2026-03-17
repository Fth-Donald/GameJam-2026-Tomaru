using UnityEngine;
using System.Collections;

public class BerrySkill : MonoBehaviour
{
    public float eTime = 10f;
    public float nSpeed = 100f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        Entity player = collision.GetComponent<Entity>();

        if (player != null)
        {
            triggered = true;
            SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
            s.enabled = false;
            StartCoroutine(BerrySkillRoutine(player));
            
        }
    }

    private IEnumerator BerrySkillRoutine(Entity player)
    {
        float originalSpeed = player.moveSpeed;

        player.moveSpeed = nSpeed;

        yield return new WaitForSeconds(eTime);

        player.moveSpeed = originalSpeed;
        Destroy(gameObject);
    }
}