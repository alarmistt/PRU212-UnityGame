using UnityEngine;
using UnityEngine.UI;

public class TaiBossHealthBar : MonoBehaviour
{
    [SerializeField] private TaiBossHealth enemyHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = enemyHealth.CurrentHealth / enemyHealth.startingHealth;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = enemyHealth.CurrentHealth / enemyHealth.startingHealth;
    }
}
