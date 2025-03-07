using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // L??ng sát th??ng c?a viên ??n

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // N?u ??n va ch?m v?i Player
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
