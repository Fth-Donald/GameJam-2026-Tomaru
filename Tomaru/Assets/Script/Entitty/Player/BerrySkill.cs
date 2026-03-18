using UnityEngine;
using System.Collections;

public class BerrySkill : MonoBehaviour
{
    

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag ("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            player.bspicked = true;
        }
        Destroy(gameObject);
    }

    
}