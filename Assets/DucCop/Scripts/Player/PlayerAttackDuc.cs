using UnityEngine;

public class PlayerAttackDuc : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackHitbox;  
    [SerializeField] private float attackRange = 0.5f; 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip swordHitSound;
    

    private Animator anim;
    private PlayerMovementDuc playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovementDuc>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManagerDuc.instance.PlaySound(swordHitSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            HealthDuc enemyHealth = enemy.GetComponent<HealthDuc>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(20);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackHitbox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitbox.position, attackRange);
    }
}
