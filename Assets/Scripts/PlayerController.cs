using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed of the player
    [SerializeField] private float jumpForce = 5f; // Force applied when jumping
    [SerializeField] private LayerMask groundLayer; // Layer for the ground
    [SerializeField] private Transform groundCheck; // Transform to check if the player is grounded
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded; // Check if the player is on the ground


    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
    }

    void Start()
    {

    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); // Check if the player is grounded
    }
    private void UpdateAnimations()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f; // Check if the player is running
        bool isJumping = !isGrounded; // Check if the player is jumping
        animator.SetBool("isRunning", isRunning); // Set the running animation
        animator.SetBool("isJumping", isJumping); // Set the jumping animation
    }
}
