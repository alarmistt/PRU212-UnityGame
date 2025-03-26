using UnityEngine;
using UnityEngine.UI;

public class TaiManaBar : MonoBehaviour
{

    [SerializeField] private TaiMana playerMana;
    [SerializeField] private Image totalManaBar; 
    [SerializeField] private Image currenthealthBar; 

    private void Start()
    {
        
        totalManaBar.fillAmount = playerMana.currentMana / playerMana.startingMana;
    }

    private void Update()
    {
        
        currenthealthBar.fillAmount = playerMana.currentMana / playerMana.startingMana;
    }
}


