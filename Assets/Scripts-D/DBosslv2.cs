using UnityEngine;
using UnityEngine.UI;

public class DBosslv2 : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isFacingRight = true;
    private Vector2 patrolStartPos;
    private Vector2 patrolEndPos;

    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform player;
    [SerializeField] private Transform detectPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private LayerMask attackLayer;

    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float chaseSpeed = 3.6f;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private float retrieveDistance = 2f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int maxHealth = 35;
    private int HealthDefault = 200;
    private int damage = 10;
    private float distance;

    void Start()
    {
        patrolStartPos = transform.position;
        patrolEndPos = new Vector2(transform.position.x + patrolDistance, transform.position.y);
    }

    void Update()
    {
        slider.value = (float)maxHealth / HealthDefault;

        if (maxHealth <= 0)
        {
            animator.SetTrigger("Die");
            Die();
            return;
        }

        if(maxHealth <= HealthDefault/2)
        {
            animator.SetBool("Heal", true);
            damage = 20;
            walkSpeed = 5f;
            chaseSpeed = 7f;
        } else
        {
            animator.SetBool("Heal", false);
        }

        if (player == null)
        {
            animator.SetBool("PlayerDead", true);
            return;
        }

        playerInRange = Vector2.Distance(transform.position, player.position) <= attackRange;

        if (playerInRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float targetX = isFacingRight ? patrolEndPos.x : patrolStartPos.x;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), walkSpeed * Time.deltaTime);

        // Nếu enemy đã đến điểm cần đổi hướng, thì quay đầu
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            Flip();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x > player.position.x && isFacingRight)
        {
            Flip();
        }
        else if (transform.position.x < player.position.x && !isFacingRight)
        {
            Flip();
        }

        if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
        {
            animator.SetBool("Attack", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Attack", true);
        }
    }

    private void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (collInfo && collInfo.GetComponent<DPlayerMovement>() != null)
        {
            collInfo.GetComponent<DPlayerMovement>().PlayerTakesDamage(damage);
        }
    }

    public void EnemyTakeDamage(int damage)
    {
        if (maxHealth <= 0) return;
        animator.SetTrigger("Damage");
        maxHealth -= damage;
    }

    private void Die()
    {
        if (coin != null) coin.SetActive(true);
        Destroy(this.gameObject, 1.8f);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.eulerAngles = new Vector3(0, isFacingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        if (detectPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(detectPoint.position, Vector2.down * distance);
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
