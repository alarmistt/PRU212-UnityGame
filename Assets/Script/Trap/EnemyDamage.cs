using UnityEngine;

using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] private float damageCooldown = 1f; 
    private bool canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canDamage)
        {
            StartCoroutine(DamageOverTime(collision.GetComponent<Health>()));
        }
    }

    private IEnumerator DamageOverTime(Health playerHealth)
    {
        canDamage = false;
        while (playerHealth != null && gameObject.activeSelf)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageCooldown);
        }
        canDamage = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canDamage = true; 
            StopAllCoroutines(); 
        }
    }
}
