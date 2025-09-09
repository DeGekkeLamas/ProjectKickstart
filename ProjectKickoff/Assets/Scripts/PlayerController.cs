using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Collider2D _collider;
    Rigidbody2D _rigidbody;
    public static PlayerController instance;

    public float moveSpeed = 1;
    public float sprintMultiplier = 2;
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
        float usedMoveSpeed = Input.GetKey(KeyCode.RightShift) || 
            Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier * moveSpeed : moveSpeed;

        _rigidbody.AddForceX(usedMoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        //transform.Translate(new(usedMoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), 0));

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundedCheck())
        {
            DoJump();
        }
    }

    bool GroundedCheck()
    {
        // Collision testing
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position - new Vector3(0, this.transform.lossyScale.x * .5f), 
            this.transform.lossyScale * .5f, 0, Vector3.down, .1f, ~LayerMask.GetMask("Player"));
        bool hitSomething = hit.collider != null;
        bool angleOutOfRange = Vector2.Dot(Vector2.up, hit.normal) <= 0;

        // Debug
        Bounds bounds = new(this.transform.position - new Vector3(0, this.transform.lossyScale.x * .5f)
            , this.transform.lossyScale);
        DebugExtension.DebugBounds(bounds, hitSomething ? Color.green : Color.red, 1);
        DebugExtension.DebugArrow(hit.point, hit.normal, angleOutOfRange ? Color.red : Color.green, 1);

        // prevent terrain like walls from returning true
        if (angleOutOfRange) return false;

        //print($"{hitSomething}");
        return hitSomething;
    }

    void DoJump()
    {
        _rigidbody.AddForce(transform.up * jumpForce);
    }
}
