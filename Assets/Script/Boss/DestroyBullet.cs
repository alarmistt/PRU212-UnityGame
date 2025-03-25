using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("explode");
            Health player = collision.GetComponent<Health>(); 
            if (player != null)
            {
                player.TakeDamage(damage); 
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground")) 
        {
            Destroy(gameObject);
        }
    }
}
