using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private MeleeEnemy meleeEnemy;
    public delegate void OnHealthChanged(float currentHealth, float maxHealth);
    public event OnHealthChanged HealthChanged;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        meleeEnemy = GetComponent<MeleeEnemy>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        HealthChanged?.Invoke(currentHealth, startingHealth);

        if (currentHealth <= 0 && meleeEnemy != null)
        {
            meleeEnemy.Die();
        }
    }
}
