using UnityEngine;

public class TaiManaPotion : MonoBehaviour
{
    public float manaAmount = 50f; 
    public AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            TaiMana playerStats = other.GetComponent<TaiMana>();
            if (playerStats != null)
            {
                playerStats.AddMana(manaAmount); 
            }

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            Destroy(gameObject); 
        }
    }
}
