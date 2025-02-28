using UnityEngine;

public class BerserkEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private int health = 3; // M�u c?a qu�i
    [SerializeField] private float attackRange = 1.5f; // Ph?m vi t?n c�ng
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
        if (isDead) return; // N?u ch?t r?i th� kh�ng l�m g� n?a

        float leftBoundary = startPosition.x - distance;
        float rightBoundary = startPosition.x + distance;

        if (!isAttacking) // Ch? di chuy?n n?u kh�ng ?ang t?n c�ng
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

        // Ki?m tra kho?ng c�ch v?i Player
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
        if (isAttacking || isDead) return; // N?u ?ang t?n c�ng th� kh�ng l�m g� c?

        isAttacking = true;
        animator.SetTrigger("meleeAttack");

        // Ng?ng di chuy?n khi t?n c�ng
        speed = 0;

        // Chuy?n v? tr?ng th�i ch?y sau khi Attack k?t th�c
        Invoke("ResetAttack", 1f); // 1 gi�y t�y theo th?i gian animation Attack
    }

    void ResetAttack()
    {
        isAttacking = false;
        speed = 2f; // Quay l?i t?c ?? di chuy?n b�nh th??ng
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
        GetComponent<Collider2D>().enabled = false; // T?t collider ?? tr�nh va ch?m
        Destroy(gameObject, 2f); // X�a qu�i sau 2 gi�y
    }
}