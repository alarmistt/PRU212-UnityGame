using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 30;
    [SerializeField] private LayerMask enemyLayers;

    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown && CanAttack())
        {
            Attack();
        }
    }

    private bool CanAttack()
    {
        return !anim.GetCurrentAnimatorStateInfo(0).IsName("die");
    }

    private void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint ch?a ???c gán trong Inspector!");
            return;
        }

        anim.SetTrigger("Attacking");
        cooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
            else if (enemy.CompareTag("Boss"))
            {
                // Thêm code x? lý ?ánh boss n?u c?n
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
