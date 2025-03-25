using UnityEngine;

public class TaiDestroyBullet : MonoBehaviour
{
    public float damage = 10f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = GetComponent<Animator>();
            anim.SetTrigger("explode");
            TaiHealth player = collision.GetComponent<TaiHealth>(); 
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
