using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float shootCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private LayerMask enemyLayers;

    private Animator anim;
    private PlayerController playerController;
    private float cooldownTimer = Mathf.Infinity;
    private bool canShoot = true;

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

        if (Input.GetKeyDown(KeyCode.F) && cooldownTimer > shootCooldown && CanAttack())
        {
            Shooting();
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Shooting()
    {
        /*playerMana.UsingMana();*/
        anim.SetTrigger("playerShooting");
        cooldownTimer = 0;


        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }
}
