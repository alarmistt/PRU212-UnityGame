using UnityEngine;

public class TaiPlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float shootCooldown = 3.0f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip shootSound;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 30;
    public LayerMask enemyLayers;

    private Animator anim;
    private TaiPlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool canShoot = true;
    private TaiMana playerMana;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<TaiPlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        playerMana = GetComponentInParent<TaiMana>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
            Attack();
    
        if (Input.GetMouseButton(1) && cooldownTimer > shootCooldown && playerMovement.CanAttack() && playerMana.currentMana > 0)
            Shooting();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("playerAttack");
        cooldownTimer = 0;

        PlaySound(attackSound);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<TaiEnemyHealth>().TakeDamage(attackDamage);
            }
            else if (enemy.CompareTag("Boss"))
            {
                enemy.GetComponent<TaiBossHealth>().TakeDamage(attackDamage);
            }
        }
    }

    private void Shooting()
    {
        playerMana.UsingMana();
        anim.SetTrigger("playerShooting");
        cooldownTimer = 0;

        PlaySound(shootSound);

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<TaiProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
