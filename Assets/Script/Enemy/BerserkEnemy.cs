using UnityEngine;

public class BerserkEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private int health = 3; // Máu c?a quái
    [SerializeField] private float attackRange = 1.5f; // Ph?m vi t?n công
    private Vector3 startPosition;
    private bool movingRight = true;
    private bool isAttacking = false;
    private bool isDead = false;

    private Animator animator;
    private Transform player;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetBool("isWalking", true); // Chuy?n t? Idle sang Run
    }

    void Update()
    {
        if (isDead) return; // N?u ch?t r?i thì không làm gì n?a

        float leftBoundary = startPosition.x - distance;
        float rightBoundary = startPosition.x + distance;

        if (!isAttacking) // Ch? di chuy?n n?u không ?ang t?n công
        {
            if (movingRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (transform.position.x >= rightBoundary)
                {
                    movingRight = false;
                    Flip();
                }
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (transform.position.x <= leftBoundary)
                {
                    movingRight = true;
                    Flip();
                }
            }
        }

        // Ki?m tra kho?ng cách v?i Player
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Attack();
        }
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Attack()
    {
        if (isAttacking || isDead) return; // N?u ?ang t?n công thì không làm gì c?

        isAttacking = true;
        animator.SetTrigger("meleeAttack");

        // Ng?ng di chuy?n khi t?n công
        speed = 0;

        // Chuy?n v? tr?ng thái ch?y sau khi Attack k?t thúc
        Invoke("ResetAttack", 1f); // 1 giây tùy theo th?i gian animation Attack
    }

    void ResetAttack()
    {
        isAttacking = false;
        speed = 2f; // Quay l?i t?c ?? di chuy?n bình th??ng
    }


    public void TakeDamage()
    {
        if (isDead) return;

        health--;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("isDie", true);
        GetComponent<Collider2D>().enabled = false; // T?t collider ?? tránh va ch?m
        Destroy(gameObject, 2f); // Xóa quái sau 2 giây
    }
}