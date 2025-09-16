using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>();
    }
   
    void Update()
    {
        animator.SetBool("isMoving", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
        animator.SetBool("isJumping", Input.GetKeyDown(KeyCode.Space) && playerController.GroundedCheck());
        animator.SetBool("isGrounded", playerController.GroundedCheck());
        animator.SetBool("isAgainstWall", playerController.IsAgainstWall());
    }
}
