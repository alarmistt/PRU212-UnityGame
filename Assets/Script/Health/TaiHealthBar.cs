using UnityEngine;
using UnityEngine.UI;

public class TaiHealthBar : MonoBehaviour
{
    [SerializeField] private TaiHealth playerHealth;
    [SerializeField] private Image totalhealthBar; 
    [SerializeField] private Image currenthealthBar; 

    private void Start()
    {
        
        totalhealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    private void Update()
    {
        
        currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }
}
