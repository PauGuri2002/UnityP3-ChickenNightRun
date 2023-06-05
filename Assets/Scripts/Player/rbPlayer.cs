using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isJumping;
    private int jumpCount = 0;
    private int maxJumpCount = 2;
    private float jumpForce = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void Update()
    {
        if (!isJumping && rb.velocity.y == 0)
        {
            jumpCount = 0;
        }
    }
}