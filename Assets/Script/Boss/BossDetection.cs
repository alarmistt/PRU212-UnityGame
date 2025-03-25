using UnityEngine;

public class BossDetection : MonoBehaviour
{
    public BossAimShoot bossScript;
    public Transform playerTransform;
    [SerializeField]public float attackRange = 10f; 

    private bool isPlayerInRange = false;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform null");
        }

        bossScript.enabled = false;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= attackRange && !isPlayerInRange)
            {
                isPlayerInRange = true;
                bossScript.enabled = true; 
            }
            else if (distanceToPlayer > attackRange && isPlayerInRange)
            {
                isPlayerInRange = false;
                bossScript.enabled = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
