using System.Collections;
using UnityEngine;

public class EnemyDamageDuc : MonoBehaviour
{
    [SerializeField] protected float damage;
    private bool isPlayerInside = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            StartCoroutine(DamageOverTime(collision.GetComponent<HealthDuc>()));
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private IEnumerator DamageOverTime(HealthDuc playerHealth)
    {
        while (isPlayerInside)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(1f); // waittime to damage
        }
    }
}
