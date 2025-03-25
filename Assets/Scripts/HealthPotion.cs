using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healAmount = 20f; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.AddHealth(healAmount); 
            Debug.Log("Healed for " + healAmount);

            Destroy(gameObject);
        }
    }
}
