using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 15f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPower = 12f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Boundary Settings")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("References")]
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float horizontalInput;
    private float velocitySmoothing; // Biến hỗ trợ SmoothDamp

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleInput();
        UpdateAnimations();
        ApplyJumpPhysics();
        LimitPlayerPosition();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
    }

    private void HandleMovement()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float smoothSpeed = Mathf.SmoothDamp(body.linearVelocity.x, targetSpeed, ref velocitySmoothing, 0.05f);

        body.linearVelocity = new Vector2(smoothSpeed, body.linearVelocity.y);

        if (horizontalInput > 0)
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        anim.SetTrigger("playerJump");
    }

    private void ApplyJumpPhysics()
    {
        if (body.linearVelocity.y < 0)
        {
            body.gravityScale = fallMultiplier;
        }
        else if (body.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            body.gravityScale = lowJumpMultiplier;
        }
        else
        {
            body.gravityScale = 1f;
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("playerWalking", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void LimitPlayerPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Max(newPosition.y, minBounds.y);
        transform.position = newPosition;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
}
