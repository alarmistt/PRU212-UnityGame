using UnityEngine;
using UnityEngine.Audio;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 50f;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerStats = other.GetComponent<Health>();
            if (playerStats != null)
            {
                playerStats.AddHealth(healAmount);

                if (pickupSound != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
                }

                Destroy(gameObject);
            }
        }
    }
}