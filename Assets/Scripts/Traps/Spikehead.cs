using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;

    private float checkTimer;
    private bool falling;

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (falling)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, range, playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);

        if (hit.collider != null)
        {
            falling = true; 
            checkTimer = 0;
        }
    }

    private void Stop()
    {
        falling = false; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject); 
        }
    }
}


//using UnityEngine;

//public class Spikehead : EnemyDamage
//{
//    [Header("SpikeHead Attributes")]
//    [SerializeField] private float speed;
//    [SerializeField] private float range;
//    [SerializeField] private float checkDelay;
//    [SerializeField] private LayerMask playerLayer;
//    private Vector3[] directions = new Vector3[4];
//    private float checkTimer;
//    private Vector3 destination;
//    private bool attacking;

//    private void OnEnable()
//    {
//        Stop();
//    }

//    private void Update()
//    {
//        if (attacking)
//        {
//            transform.Translate(destination * Time.deltaTime * speed);
//        }
//        else
//        {
//            checkTimer += Time.deltaTime;
//            if (checkTimer > checkDelay)
//            {
//                CheckForPlayer();
//            }
//        }
//    }

//    private void CheckForPlayer()
//    {
//        CalculateDirections();

//        check if spikehead sees player in all 4 directions
//        for (int i = 0; i < directions.Length; i++)
//        {
//            Debug.DrawRay(transform.position, directions[i], Color.red);
//            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

//            if (hit.collider != null && !attacking)
//            {
//                attacking = true;
//                destination = directions[i];
//                checkTimer = 0;
//            }
//        }
//    }

//    private void CalculateDirections()
//    {
//        directions[0] = transform.right * range; // Right direction
//        directions[1] = -transform.right * range; // Left direction
//        directions[2] = transform.up * range; // Up direction
//        directions[3] = -transform.up * range; // Down direction
//    }

//    private void Stop()
//    {
//        destination = transform.position; //Set destination as current position so it doesn't move
//        attacking = false;
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        base.OnTriggerEnter2D(collision);
//        Stop(); //Stop spikehead once he hits something
//    }
//}
