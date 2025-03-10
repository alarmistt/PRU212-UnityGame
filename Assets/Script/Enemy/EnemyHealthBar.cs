using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
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

