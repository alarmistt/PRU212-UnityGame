using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    //public void SetMaxHealth(float health)
    //{
    //    slider.maxValue = health;
    //    slider.value = health;
    //}
    public void SetHealth(float health)
    {
        slider.value = health;
    }

    

    public void SetHealthSpecial(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health;
        slider.maxValue = maxHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, slider.normalizedValue);
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
    //[SerializeField] private Health playerHealth;
    //[SerializeField] private Image totalhealthBar;
    //[SerializeField] private Image currenthealthBar;

    //private void Start()
    //{
    //    totalhealthBar.fillAmount = playerHealth.currentHealth;
    //}

    //private void Update()
    //{
    //    currenthealthBar.fillAmount = playerHealth.currentHealth;   
    //}
}
