using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DPlayerMovement : MonoBehaviour
{
    private float movement;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    private bool isWon = false;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 8f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [SerializeField]
    private int dame = 5;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpHeight = 11f;
    [SerializeField]
    private float attackRadius = 1.5f;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int maxMana = 100;
    [SerializeField]
    private int curentCoin = 0;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject victoryUI;
    [SerializeField]
    private GameObject PauseUI;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform attackPoint;  
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private TrailRenderer tr;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image ManaBar;
    [SerializeField]
    private Text currentCoinText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        curentCoin = PlayerPrefs.GetInt("CurentCoin", 0);
        UpdateCoinUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowPauseUI();
        }
        if (Input.GetKey(KeyCode.L) && canDash && maxMana >= 5)
        {
            UseMana(5);
            StartCoroutine(Dash());
        }
        if (isWon)
        {
            animator.SetFloat("Walk", 0);
            movement = 0f;
            speed = 0f;
            return;
        }

        if (maxHealth <= 0)
        {
            Die();
        }

        //maxHealthText.text = maxHealth.ToString();
        healthBar.fillAmount = (float)maxHealth / 100f;
        ManaBar.fillAmount = (float)maxMana / 100f;

        currentCoinText.text = curentCoin.ToString() + "/40";

        movement = Input.GetAxis("Horizontal");

        if (movement < 0f && isFacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (movement > 0f && !isFacingRight)
        {
            transform.eulerAngles = Vector3.zero;
            isFacingRight = true;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
            animator.SetBool("Jump",true);
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.J))
        {
            animator.SetTrigger("Attack");
        }

        //animations
        if (Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("Walk", 1f);
        }
        else if (Mathf.Abs(movement) < 0.1f)
        {
            animator.SetFloat("Walk", 0f);
        }

    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement * Time.fixedDeltaTime * speed, 0f, 0f) ;
    }

    void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = jumpHeight;
        rb.linearVelocity = velocity;
    }

    private void PlayerAttack()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetLayer);
        FindObjectOfType<DSounds>().PlaySwordSound();
        if (hitInfo)
        {
            if (hitInfo.GetComponent<DEnemy>() != null)
            {
                hitInfo.GetComponent<DEnemy>().EnemyTakeDamage(dame);
            }

            if (hitInfo.GetComponent<DEnemy21>() != null)
            {
                hitInfo.GetComponent<DEnemy21>().EnemyTakeDamage(dame);
            }

            if (hitInfo.GetComponent<DBosslv2>() != null)
            {
                hitInfo.GetComponent<DBosslv2>().EnemyTakeDamage(dame);
            }
        }
    }

    public bool canAttack()
    {
        if (maxMana > 5 && movement == 0 && isGrounded)
        {
            return true;
        }
        
        return false;
    }

    public void UseMana(int mana)
    {
        if (maxMana <= 0)
        {
            return;
        }
        maxMana -= mana;
    }

    public void HealHealth(int heal)
    {
        if (heal <= 0)
        {
            return;
        }
        maxHealth += heal;
    }

    public void HealManaHealth(int heal)
    {
        if (heal <= 0)
        {
            return;
        }
        maxHealth = heal;
        maxMana = heal;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            curentCoin++;
            PlayerPrefs.SetInt("CurentCoin", curentCoin);
            PlayerPrefs.Save();
            collision.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Collect");
            FindObjectOfType<DSounds>().PlayCoinSound();
            Destroy(collision.gameObject, 1f);
        }    
        
        if (collision.gameObject.tag == "Trap")
        {
            Die();
        }

        if (collision.gameObject.tag == "Key")
        {
            victoryUI.gameObject.SetActive(true);
            isWon = true;
            Destroy(collision.gameObject);
        }
    }

    public void PlayerTakesDamage(int damage)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        maxHealth -= damage;
    }

    public void Die()
    {
        //Debug.Log(this.transform.name + "die");
        animator.SetTrigger("Die");

        Invoke(nameof(ShowGameOverUI), 1.5f);

        Destroy(this.gameObject, 1.6f);

        //gameOverUI.SetActive(true);
    }

    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    private void ShowPauseUI()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void UpdateCoinUI()
    {
        currentCoinText.text = curentCoin.ToString() + "/40";
    }

    public void SetCurrentCoin()
    {
        PlayerPrefs.SetInt("curentCoin", 0);
        PlayerPrefs.Save();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2((isFacingRight ? 1 : -1) * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
