using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerDie = other.GetComponent<PlayerRespawn>();
            Animator anim  = other.GetComponent<Animator>();
            if (playerDie != null)
            {
                playerDie.Die( anim); 
            }
        }
    }
}
