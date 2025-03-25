using UnityEngine;
using UnityEngine.Audio;

public class TaiHealthPotion : MonoBehaviour
{
    public float healAmount = 50f;
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TaiHealth playerStats = other.GetComponent<TaiHealth>();
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