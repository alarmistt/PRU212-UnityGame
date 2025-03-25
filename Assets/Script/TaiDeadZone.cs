using UnityEngine;

public class TaiDeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TaiPlayerRespawn playerDie = other.GetComponent<TaiPlayerRespawn>();
            Animator anim  = other.GetComponent<Animator>();
            if (playerDie != null)
            {
                playerDie.Die(); 
            }
        }
    }
}
