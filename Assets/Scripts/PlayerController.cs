using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 4.5f;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveX, 0, moveZ);
        
        // Only move and rotate if input is pressed
        if (move.magnitude > 0.1f)
        {
            // Move character
            transform.position += move * moveSpeed * Time.deltaTime;

            // Rotate character to face movement direction
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.1f);
        }

        // Update animator speed (used to blend Idle <-> Walk)
        animator.SetFloat("Speed", move.magnitude);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;

            // Trigger jump animation first
            animator.SetBool("isJumping", true);

            // Call actual jump after short delay
            Invoke(nameof(PerformJump), 0.3f); // adjust timing as needed
        }
    }

    void PerformJump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // Stop jump animation when grounded
            animator.SetBool("isJumping", false);
        }
    }
}
