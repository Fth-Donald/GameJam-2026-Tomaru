using UnityEngine;

public class Entity : MonoBehaviour
{
    protected bool isDead;

    protected virtual void Awake()
    {
        Health HP = this.GetComponent<Health>();
        isDead = HP.isDead;
    }

    protected virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        Destroy(gameObject);
    }

}