using UnityEngine;

public class EnemyDamageDuc : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<HealthDuc>().TakeDamage(damage);
        }
    }
}
