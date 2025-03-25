using UnityEngine;

public class BossAimShoot : MonoBehaviour
{
    [Header("Shooting Configuration")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player;
    public float bulletSpeed = 5f;
    public float defaultFireRate = 1.5f;
    public float enrageFireRate = 1f;
    public float attackRange = 5f;
    public float detectRange = 10f;

    [Header("Movement Configuration")]
    public float moveSpeed = 4f;
    public float enrageMoveSpeed = 6f;
    private Rigidbody2D rb;
    private bool isChasing = false;

    [Header("Animation Configuration")]
    public Animator animator;
    private float baseAnimationSpeed = 1f;
    private float fireRate;

    private float nextFireTime;
    private bool isDead = false;
    private BossHealth bossHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
        fireRate = defaultFireRate;

        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("distance", distance);

        // Enrage mode when health drops
        if (bossHealth.CurrentHealth <= 2300)
        {
            fireRate = enrageFireRate;
            moveSpeed = enrageMoveSpeed;
        }

        if (distance <= attackRange)
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
            Attack();
        }
        else if (distance <= detectRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    void Attack()
    {
        if (isDead) return;

        if (Time.time > nextFireTime)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack"))
            {
                animator.speed = baseAnimationSpeed * (1.5f / fireRate);
            }
            else
            {
                animator.speed = baseAnimationSpeed;
            }
            animator.SetTrigger("isAttack");
            nextFireTime = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        if (isDead || player == null) return;

        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        if (rbBullet != null)
        {
            rbBullet.linearVelocity = direction * bulletSpeed;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;
        FlipBoss(moveDirection.x);
    }

    void FlipBoss(float directionX)
    {
        if ((directionX < 0 && transform.localScale.x > 0) || (directionX > 0 && transform.localScale.x < 0))
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("isDie", true);
        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        Destroy(gameObject, 2f);
    }

    public void UpdatePlayerReference(Transform newPlayer)
    {
        player = newPlayer;
    }

    public void ResetBoss()
    {
        // Check and reassign components if needed
        if (bossHealth == null)
        {
            bossHealth = GetComponent<BossHealth>();
            if (bossHealth == null)
            {
                Debug.LogError("BossHealth component not found!");
                return;
            }
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found!");
                return;
            }
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component not found!");
                return;
            }
        }

        // Reset health
        bossHealth.CurrentHealth = bossHealth.startingHealth;

        // Reset animation
        animator.SetBool("isDie", false);
        animator.speed = baseAnimationSpeed;
        animator.Play("Idle");

        // Reset states
        isDead = false;
        fireRate = defaultFireRate;
        moveSpeed = 2f;  // Reset move speed
        isChasing = false;
        rb.linearVelocity = Vector2.zero;

        Debug.Log("Boss has been successfully reset!");
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
