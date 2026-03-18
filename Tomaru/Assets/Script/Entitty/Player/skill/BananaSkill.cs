using UnityEngine;
using System.Collections;

public class BananaSkill : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.skill1 == 0)
            {
                player.skill1 = 4;
                Destroy(gameObject);
                return;
            }
            else if (player.skill2 == 0)
            {
                player.skill2 = 4;
                Destroy(gameObject);
                return;
            }
            else return;
        }
    }
}