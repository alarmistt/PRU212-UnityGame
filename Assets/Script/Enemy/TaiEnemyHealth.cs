using UnityEngine;

public class TaiEnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private TaiMeleeEnemy meleeEnemy;
    private TaiShootingEnemy rangedEnemy;
    private TaiBossAimShoot bossAimShoot;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        meleeEnemy = GetComponent<TaiMeleeEnemy>();
        rangedEnemy = GetComponent<TaiShootingEnemy>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("isHurt");
        }
        else
        {
            if(meleeEnemy != null)
            meleeEnemy.Die();
            if(rangedEnemy != null)
                rangedEnemy.Die();
          
        }
    }
    
}

