using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
   
    void Update()
    {
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", Input.GetKeyDown(KeyCode.Space) && playerController.GroundedCheck());
        animator.SetBool("isGrounded", playerController.GroundedCheck());
        animator.SetBool("isAgainstWall", playerController.IsAgainstWall());

        if (isMoving) spriteRenderer.flipX = Input.GetAxis("Horizontal") < 0;
    }
}
