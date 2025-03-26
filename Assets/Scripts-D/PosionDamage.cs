using UnityEngine;

public class PosionDamage : MonoBehaviour
{
    [SerializeField] private float damageValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<DSounds>().HealManaheath();
            collision.GetComponent<DPlayerMovement>().DamagePosion((int)damageValue);
            gameObject.SetActive(false);
        }
    }
}
