using UnityEngine;
using UnityEngine.UI;

public class DEnemy : MonoBehaviour
{
    private bool playerInRange = false;
    private bool isFacingLeft = true;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform detectPoint;

    [SerializeField]
    private Transform attackPonit;

    [SerializeField]
    private LayerMask detectLayer;

    [SerializeField]
    private LayerMask attackLayer;

    [SerializeField]
    private float attackRange = 2.5f;

    [SerializeField]
    private float walkSpeed = 1.5f;

    [SerializeField]
    private float chaseSpeed = 3.6f;

    [SerializeField]
    private float retrieveDistance = 2f;

    [SerializeField]
    private float attackRadius = 2f;

    [SerializeField]
    private int maxHealth = 15;

    
    private int HealthDefault = 15;

    [SerializeField]
    private float distance;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float) maxHealth / HealthDefault;

        if (maxHealth <= 0)
        {
            Die();
        }

        if (player == null)
        {
            animator.SetBool("PlayerDead", true);
            return;
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            if (transform.position.x < player.position.x && isFacingLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isFacingLeft = false;
            } else if (transform.position.x > player.position.x && !isFacingLeft)
            {
                transform.eulerAngles = Vector3.zero;
                isFacingLeft = true;
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
        else
        {
            //Debug.Log("Player in not Attack Range");
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(detectPoint.position, Vector2.down, distance, detectLayer);

            if (hit == false)
            {
                if (isFacingLeft)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    isFacingLeft = false;
                }
                else if (!isFacingLeft)
                {
                    transform.eulerAngles = Vector3.zero;
                    isFacingLeft = true;
                }
            }

            
        }
    }

    private void Attack()
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPonit.position, attackRadius, attackLayer);

        if (collInfo)
        {
            if (collInfo.GetComponent<DPlayerMovement>() !=null )
            {
                collInfo.GetComponent<DPlayerMovement>().PlayerTakesDamage(5);
            }
        }
    }

    public void  EnemyTakeDamage(int damage)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        animator.SetTrigger("Damage");
        maxHealth -= damage;
    }

    private void Die()
    {
        Debug.Log(this.gameObject.name + "died");
        animator.SetTrigger("Die");
        if (coin != null)
        {
            coin.SetActive(true);
        }
        
        Destroy(this.gameObject, 1.8f);
        

    }

    private void OnDrawGizmosSelected()
    {
        if (detectPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(detectPoint.position, Vector2.down * distance);

        if (attackPonit != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPonit.position, attackRadius);
        }
    }
}
