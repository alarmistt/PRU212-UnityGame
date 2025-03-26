using UnityEngine;

public class PosionHeal : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<DSounds>().HealManaheath();
            collision.GetComponent<DPlayerMovement>().HealPosion((int)healthValue);
            gameObject.SetActive(false);
        }
    }
}
