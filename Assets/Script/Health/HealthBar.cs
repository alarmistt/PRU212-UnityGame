using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;  // Tham chi?u ??n Health c?a nhân v?t
    [SerializeField] private Image totalhealthBar; // Thanh máu t?ng (vi?n ngoài)
    [SerializeField] private Image currenthealthBar; // Thanh máu hi?n t?i

    private void Start()
    {
        // C?p nh?t t?ng thanh máu theo t? l? máu t?i ?a
        totalhealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    private void Update()
    {
        // C?p nh?t thanh máu hi?n t?i theo máu còn l?i
        currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }
}
