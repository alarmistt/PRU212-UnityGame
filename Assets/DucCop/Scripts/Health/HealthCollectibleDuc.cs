using UnityEngine;

public class HealthCollectibleDuc : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //SoundManagerDuc.instance.PlaySound(pickupSound);
            //collision.GetComponent<HealthDuc>().AddHealth(healthValue);
            //gameObject.SetActive(false);
            collision.GetComponent<DPlayerMovement>().HealHealth((int)healthValue);
            gameObject.SetActive(false);
        }
    }
}
