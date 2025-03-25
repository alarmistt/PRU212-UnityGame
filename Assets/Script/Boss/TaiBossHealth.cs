using UnityEngine;

public class TaiBossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float CurrentHealth { get; set; }

    private Animator anim;
    private TaiBossAimShoot bossAimShoot;
    private bool isDead = false;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        anim = GetComponent<Animator>();
        bossAimShoot = GetComponent<TaiBossAimShoot>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; 

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);

        if (CurrentHealth > 0)
        {
            anim.SetTrigger("isHurt");
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        if (bossAimShoot != null)
            bossAimShoot.Die();
    }
}
