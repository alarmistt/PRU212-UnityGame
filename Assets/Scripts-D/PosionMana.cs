using UnityEngine;

public class PosionMana : MonoBehaviour
{
    [SerializeField] private float manaValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<DSounds>().HealManaheath();
            collision.GetComponent<DPlayerMovement>().ManaPosion((int)manaValue);
            gameObject.SetActive(false);
        }
    }
}
