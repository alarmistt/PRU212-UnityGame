using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;  // Tham chi?u ??n Health c?a nh�n v?t
    [SerializeField] private Image totalhealthBar; // Thanh m�u t?ng (vi?n ngo�i)
    [SerializeField] private Image currenthealthBar; // Thanh m�u hi?n t?i

    private void Start()
    {
        // C?p nh?t t?ng thanh m�u theo t? l? m�u t?i ?a
        totalhealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    private void Update()
    {
        // C?p nh?t thanh m�u hi?n t?i theo m�u c�n l?i
        currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }
}
