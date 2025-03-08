using UnityEngine;

public class BossDetection : MonoBehaviour
{
    public BossAimShoot bossScript; // Tham chi?u ??n script t?n c�ng c?a Boss
    public Transform playerTransform; // Tham chi?u ??n Transform c?a Player
    [SerializeField]public float attackRange = 10f; // Ph?m vi t?n c�ng c?a Boss

    private bool isPlayerInRange = false;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform ch?a ???c g�n trong Inspector.");
        }

        bossScript.enabled = false; // Ban ??u t?t ch?c n?ng t?n c�ng
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange && !isPlayerInRange)
            {
                isPlayerInRange = true;
                bossScript.enabled = true; // K�ch ho?t t?n c�ng
            }
            else if (distanceToPlayer > attackRange && isPlayerInRange)
            {
                isPlayerInRange = false;
                bossScript.enabled = false; // Ng?ng t?n c�ng
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // V? m?t h�nh tr�n trong Scene View ?? hi?n th? ph?m vi t?n c�ng c?a Boss
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
