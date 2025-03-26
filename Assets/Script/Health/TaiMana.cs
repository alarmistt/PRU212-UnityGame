using UnityEngine;

public class TaiMana : MonoBehaviour
{
    [Header("Mana")]
    [SerializeField] public float startingMana;
    public float currentMana { get; private set; }
    public float manaPershoot = 10f;

    private TaiPlayerAttack playerAttack;
    private void Awake()
    {
        playerAttack = GetComponentInParent<TaiPlayerAttack>();
        currentMana = startingMana;
    }
    public void UsingMana()
    {
        currentMana = Mathf.Clamp(currentMana - manaPershoot, 0, startingMana);
    }

    public void AddMana(float _value)
    {
        currentMana = Mathf.Clamp(currentMana + _value, 0, startingMana);
    }
    public void RestoreMana()
    {
        currentMana = startingMana;
    }
}
