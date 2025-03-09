using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnDelay = 1.5f;

    private Animator anim;
    private Health health;
    private Vector3 initialRespawnPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        if (respawnPoint != null)
        {
            initialRespawnPosition = respawnPoint.position;
        }
    }

    public void Die(Animator anim)
    {
        if (anim != null)
        {
            anim.SetTrigger("playerDie");
        }
        StartCoroutine(Respawn());
    }
    public void Die()
    {
        if (anim != null)
        {
            anim.SetTrigger("playerDie");
        }
        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);

        transform.position = initialRespawnPosition;

        if (health != null)
        {
            health.RestoreHealth();
        }
        if (anim != null)
        {
            anim.ResetTrigger("playerDie");
            anim.Play("PlayerIdle");
        }
    }
}
