using UnityEngine;

public class DPlayerAttackFireball : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    //[SerializeField]
    //private LayerMask targetLayer;

    private Animator anim;
    private DPlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<DPlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.K) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            AttackFireBall();
            playerMovement.UseMana(5);
        }
            

        cooldownTimer += Time.deltaTime;
    }

    private void AttackFireBall()
    {
        anim.SetTrigger("AttackFireball");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        float direction = transform.eulerAngles.y == 0 ? 1f : -1f;
        fireballs[FindFireball()].GetComponent<DFireball>().SetDirection(direction);

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
}
