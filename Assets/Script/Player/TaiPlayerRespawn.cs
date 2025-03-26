using UnityEngine;
using System.Collections;

public class TaiPlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    public Transform bossRespawnPoint; // Gán trong Inspector
    public TaiBossAimShoot bossAimShoot; // Gán trong Inspector
    public TaiGateTrigger gateTrigger; // Gán trong Inspector
    public float respawnDelay = 1.5f;
    [SerializeField] private AudioClip checkpoint;
    private Animator anim;
    private TaiHealth health;
    private TaiMana mana;
    private Vector3 initialRespawnPosition;

    private void Awake()
    {
        // Khởi tạo các component
        anim = GetComponent<Animator>();
        health = GetComponent<TaiHealth>();
        mana = GetComponent<TaiMana>();

        // Gán currentCheckpoint mặc định
        currentCheckpoint = transform;
        initialRespawnPosition = transform.position;
    }

    public void Die()
    {
        // Kiểm tra và chạy animation nếu Animator tồn tại
        if (anim != null)
        {
            anim.SetTrigger("playerDie");
        }

        // Dừng vận tốc của Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Vô hiệu hóa TaiPlayerMovement
        TaiPlayerMovement movement = GetComponent<TaiPlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }

        // Bắt đầu coroutine Respawn
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        // Đợi một khoảng thời gian
        yield return new WaitForSeconds(respawnDelay);

        // Đặt lại vị trí player về checkpoint
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
        }
        else
        {
            transform.position = initialRespawnPosition;
        }

        // Mở cổng nếu gateTrigger tồn tại
        if (gateTrigger != null)
        {
            gateTrigger.OpenGate();
        }

        // Xử lý boss nếu tồn tại
        if (bossAimShoot != null)
        {
            bossAimShoot.UpdatePlayerReference(transform);
            if (bossRespawnPoint != null)
            {
                bossAimShoot.transform.position = bossRespawnPoint.position;
            }
            bossAimShoot.ResetBoss();
        }

        // Khôi phục máu từ từ
        float restoreTime = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < restoreTime)
        {
            if (health != null)
            {
                float t = elapsedTime / restoreTime;
                health.AddHealth(Mathf.Lerp(0, health.startingHealth, t));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Khôi phục mana và health đầy đủ
        if (mana != null)
        {
            mana.RestoreMana();
        }
        if (health != null)
        {
            health.RestoreHealth();
        }

        // Đặt lại animation
        if (anim != null)
        {
            anim.ResetTrigger("playerDie");
            anim.Play("PlayerIdle");
        }

        // Kích hoạt lại TaiPlayerMovement
        TaiPlayerMovement movement = GetComponent<TaiPlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;

            // Phát âm thanh checkpoint nếu có
            if (checkpoint != null)
            {
                AudioSource.PlayClipAtPoint(checkpoint, transform.position);
            }

            // Vô hiệu hóa collider và chạy animation của checkpoint
            Collider2D col = collision.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }

            Animator checkpointAnim = collision.GetComponent<Animator>();
            if (checkpointAnim != null)
            {
                checkpointAnim.SetTrigger("appear");
            }
        }
    }
}