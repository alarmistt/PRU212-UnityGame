using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackHitbox;  
    [SerializeField] private float attackRange = 0.5f; 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip swordHitSound;
    

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
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
        SoundManager.instance.PlaySound(swordHitSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackHitbox.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
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
