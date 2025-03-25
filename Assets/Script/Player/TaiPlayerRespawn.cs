using UnityEngine;
using System.Collections;

public class TaiPlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform bossRespawnPoint;
    public TaiBossAimShoot bossAimShoot;
    public TaiGateTrigger gateTrigger;
    public float respawnDelay = 1.5f;
    private Animator anim;
    private TaiHealth health;
    private Vector3 initialRespawnPosition;

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<TaiHealth>();

        if (health == null)
        {
            Debug.LogError("Không tìm thấy component Health trên đối tượng Player!");
        }

        if (anim == null)
        {
            Debug.LogError("Không tìm thấy component Animator trên đối tượng Player!");
        }

        if (respawnPoint != null)
        {
            initialRespawnPosition = respawnPoint.position;
        }
    }


    public void Die(Animator anim)
    {
        if (anim != null)
        {
            anim.SetTrigger("playerDie");
        }
        StartCoroutine(Respawn());
    }
    public void Die()
    {
        if (anim != null)
        {
            // Đặt trigger trạng thái chết
            anim.SetTrigger("playerDie");
            // Vô hiệu hóa di chuyển để không bị chuyển sang các trạng thái khác
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<TaiPlayerMovement>().enabled = false;
        }

        StartCoroutine(Respawn());
    }
    private IEnumerator Respawn()
    {
        gateTrigger.OpenGate();
        // Dịch chuyển về vị trí hồi sinh
        transform.position = initialRespawnPosition;

        // Tìm lại Boss và reset trạng thái
        if (bossAimShoot != null)
        {
            bossAimShoot.UpdatePlayerReference(transform);

            // Đặt boss về vị trí hồi sinh
            if (bossRespawnPoint != null)
            {
                bossAimShoot.transform.position = bossRespawnPoint.position;
                Debug.Log("Boss đã quay về vị trí hồi sinh!");
            }
            bossAimShoot.ResetBoss(); // Reset lại boss về trạng thái ban đầu
        }
        else
        {
            Debug.LogWarning("Không tìm thấy đối tượng BossAimShoot!");
        }

        // Hồi máu dần dần để tạo cảm giác tự nhiên
        float restoreTime = 1f; // Thời gian hồi máu
        float elapsedTime = 0f;
        while (elapsedTime < restoreTime)
        {
            float t = elapsedTime / restoreTime;
            health.AddHealth(Mathf.Lerp(0, health.startingHealth, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        health.RestoreHealth();

        // Hiệu ứng hiện dần (Fade in)
        if (anim != null)
        {
            anim.ResetTrigger("playerDie");
            anim.Play("PlayerIdle");
        }
         GetComponent<TaiPlayerMovement>().enabled = true;

        Debug.Log("Hồi sinh hoàn tất!");
    }



}
