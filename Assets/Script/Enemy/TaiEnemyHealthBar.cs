using UnityEngine;
using UnityEngine.UI;

public class TaiEnemyHealthBar : MonoBehaviour
{
    [SerializeField] private TaiEnemyHealth enemyHealth;
    [SerializeField] private Image totalhealthBar; 
    [SerializeField] private Image currenthealthBar; 

    private void Start()
    {
        totalhealthBar.fillAmount = enemyHealth.currentHealth / enemyHealth.startingHealth;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = enemyHealth.currentHealth / enemyHealth.startingHealth;
    }
}

