using UnityEngine;

public class WASDController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;
    public float crouchHeight = 0.5f;
    public float maxStamina = 100f;
    public float staminaUseRate = 30f;
    public float staminaRegenRate = 10f;
    public float movementThreshold = 0.1f;
    private float currentStamina;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private bool isGrounded;
    private bool isCrouching;
    private Vector2 originalColliderSize;
    private Animator animator;
    private float moveDirection;
    private bool isExiting;
    private float lastInputTime;
    public float exitDelay = 0.5f;
    private bool isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        currentStamina = maxStamina;
        animator = GetComponent<Animator>();
        lastInputTime = Time.time;
    }

    void Update()
    {
        // Get input for A and D keys
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
        }
        else
        {
            moveDirection = 0f;
        }

        isMoving = Mathf.Abs(moveDirection) > movementThreshold;

        // Running (uses stamina)
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isCrouching && isMoving;

        // Update last input time and exiting state
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift))
        {
            lastInputTime = Time.time;
            isExiting = false;
        }
        else if (Time.time - lastInputTime > exitDelay)
        {
            isExiting = true;
        }

        if (isRunning)
        {
            UseStamina(staminaUseRate * Time.deltaTime);
        }
        else
        {
            RegenerateStamina();
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isCrouching)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        // Crouching
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            Crouch();
        }
        else if (!Input.GetKey(KeyCode.S) && isCrouching)
        {
            StopCrouching();
        }

        // Update animator parameters
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isCrouching", isCrouching);
        animator.SetBool("isExiting", isExiting);

        // Flip the character sprite based on movement direction
        if (isMoving)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }
    }

    void FixedUpdate()
    {
        // Apply movement in FixedUpdate for consistent physics
        if (isMoving)
        {
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 ? runSpeed : moveSpeed;
            rb.velocity = new Vector2(moveDirection * currentSpeed, rb.velocity.y);
        }
        else
        {
            // Stop horizontal movement if not moving
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
            boxCollider.offset = new Vector2(boxCollider.offset.x, -((originalColliderSize.y - crouchHeight) / 2));
        }
    }

    void StopCrouching()
    {
        if (isCrouching)
        {
            isCrouching = false;
            boxCollider.size = originalColliderSize;
            boxCollider.offset = Vector2.zero;
        }
    }

    void UseStamina(float amount)
    {
        currentStamina = Mathf.Max(0f, currentStamina - amount);
    }

    void RegenerateStamina()
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.deltaTime);
    }

    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }
}