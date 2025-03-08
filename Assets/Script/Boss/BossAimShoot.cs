using UnityEngine;

public class BossAimShoot : MonoBehaviour
{
    [Header("Cấu hình bắn")]
    public GameObject bulletPrefab; // Prefab đạn
    public Transform firePoint; // Vị trí bắn
    public Transform player; // Vị trí người chơi
    public float bulletSpeed = 5f; // Tốc độ đạn
    public float fireRate = 1.5f; // Tốc độ bắn (giây)
    public float attackRange = 5f; // Phạm vi bắn
    public float detectRange = 10f; // Phạm vi phát hiện (đuổi theo)

    [Header("Cấu hình di chuyển")]
    public float moveSpeed = 2f; // Tốc độ di chuyển
    private Vector2 moveDirection;
    private bool isChasing = false; // Biến kiểm tra trạng thái di chuyển

    [Header("Cấu hình animation")]
    public Animator animator;
    private float baseAnimationSpeed = 1f; // Tốc độ animation mặc định

    private float nextFireTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // Boss không bị rơi xuống
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("distance", distance);

        if (distance <= attackRange)
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero; // Dừng di chuyển
            Attack();
        }
        else if (distance <= detectRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero; // Dừng lại nếu ngoài phạm vi phát hiện
        }

        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        moveDirection = (player.position - transform.position).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;

        // Quay mặt về hướng Player
        FlipBoss(moveDirection.x);
    }

    void Attack()
    {
        if (Time.time > nextFireTime)
        {
            // Điều chỉnh tốc độ animation theo tốc độ bắn
            animator.speed = baseAnimationSpeed * (1.5f / fireRate);
            animator.SetTrigger("isAttack");

            // Cập nhật thời gian tấn công tiếp theo
            nextFireTime = Time.time + fireRate;
        }
    }

    public void Shoot() // Gọi từ Animation Event
    {
        if (player == null) return;

        // Hướng bắn chính xác từ giữa Boss
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        if (rbBullet != null)
        {
            rbBullet.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D không được tìm thấy trên viên đạn.");
        }
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

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("isHurt");
    }
}
