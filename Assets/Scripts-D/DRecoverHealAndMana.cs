using UnityEngine;

public class DRecoverHealAndMana : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<DSounds>().HealManaheath();
            collision.GetComponent<DPlayerMovement>().HealManaHealth((int)healthValue);
            gameObject.SetActive(false);
        }
    }
}
