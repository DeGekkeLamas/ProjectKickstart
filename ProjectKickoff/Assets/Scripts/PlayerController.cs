using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Collider2D _collider;
    Rigidbody2D _rigidbody;
    public static PlayerController instance;

    public float moveSpeed = 1;
    public float sprintMultiplier;
    public float jumpForce = 1;
    void Awake()
    {
        instance = this;
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Walk
        float usedMoveSpeed = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier * moveSpeed : moveSpeed; 
        transform.Translate( new(usedMoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0) );

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundedCheck())
        {
            DoJump();
        }
    }

    bool GroundedCheck()
    {
        /// Collision testing
        bool collisionLeft = Physics2D.Raycast(
            this.transform.position - .49f * new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0),
            Vector3.down, .1f, ~LayerMask.GetMask("Player"));
        bool collisionRight = Physics2D.Raycast(
            this.transform.position - .49f * new Vector3(this.transform.localScale.x, this.transform.localScale.y, 0),
            Vector3.down, .1f, ~LayerMask.GetMask("Player"));

        Debug.DrawRay(this.transform.position - .49f * new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0),
            Vector3.down * .1f, collisionLeft ? Color.green : Color.red, 1);
        Debug.DrawRay(this.transform.position - .49f * new Vector3(this.transform.localScale.x, this.transform.localScale.y, 0),
            Vector3.down * .1f, collisionRight ? Color.green : Color.red, 1);

        print($"{collisionLeft}, {collisionRight}");
        return collisionLeft || collisionRight;
    }

    void DoJump()
    {
        _rigidbody.AddForce(transform.up * jumpForce);
    }
}
