using UnityEngine;

public class DFireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage = 10;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.position += new Vector3(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DEnemy enemy = collision.GetComponent<DEnemy>();
            if (enemy != null)
            {
                enemy.EnemyTakeDamage(damage);
            }
            
            DEnemy21 enemy21 = collision.GetComponent<DEnemy21>();
            if (enemy21 != null)
            {
                enemy21.EnemyTakeDamage(damage);
            }
        }

        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        transform.eulerAngles = new Vector3(0f, _direction == -1 ? 180f : 0f, 0f);
    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
