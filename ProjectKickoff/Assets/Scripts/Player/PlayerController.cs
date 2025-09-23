using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Collider2D _collider;
    Rigidbody2D _rigidbody;
    PhysicsMaterial2D bouncyPhysicsMaterial;
    public static PlayerController instance;
    [HideInInspector] public Vector3 spawnPoint;
    [HideInInspector] public Vector3 worldSpawn;

    public float moveSpeed = 1;
    public float sprintMultiplier = 2;
    public float jumpForce = 1;
    [Tooltip("Multiplies with jumpforce, set to 1 to be the same as jumpforce")]
    public float wallJumpIntensity = .5f;

    [Header("Events")]
    public UnityEvent onJump;
    void OnEnable()
    {
        instance = this;
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        spawnPoint = transform.position;
        worldSpawn = transform.position;
    }
    private void OnDisable()
    {
        SpawnpointPlatform.SetAllInactive();
    }

    void Update()
    {
        // Walk
        float movement = Input.GetAxis("Horizontal");
        float usedMoveSpeed = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier * moveSpeed : moveSpeed;
        DoMove(movement * usedMoveSpeed * Time.deltaTime * Vector2.right);
        

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GroundedCheck())
            {
                DoJump();
            }
            // Walljump
            else
            {
                RaycastHit2D hit;
                if (movement > 0) hit = TerrainCheckRight();
                else hit = TerrainCheckLeft();
                if (hit.collider != null)
                {
                    WallJump(hit.normal);
                }
            }
        }

        // Check for death
        if (this.transform.position.y < -100) Death();
    }

    public bool GroundedCheck()
    {
        // Collision testing
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, 
            this.transform.lossyScale - new Vector3(this.transform.lossyScale.x * .5f, 0)
            , 0, Vector3.down, .1f, ~LayerMask.GetMask("Player"));
        bool hitSomething = hit.collider != null;
        if (hitSomething) if (hit.collider.tag == "Not_Jumpable") hitSomething = false;//so the player can't jump off of the trampoline card and the eruption card.
        bool angleOutOfRange = Vector2.Dot(Vector2.up, hit.normal) <= 0;

        // Debug
        Bounds bounds = new(this.transform.position
            , this.transform.lossyScale - new Vector3(this.transform.lossyScale.x * .5f, 0));
        DebugExtension.DebugBounds(bounds, hitSomething ? Color.green : Color.red);
        DebugExtension.DebugArrow(hit.point, hit.normal, angleOutOfRange ? Color.red : Color.green);

        // prevent terrain like walls from returning true
        if (angleOutOfRange) return false;

        //print($"{hitSomething}");
        return hitSomething;
    }

    void DoJump()
    {
        _rigidbody.AddForce(transform.up * jumpForce);
        onJump.Invoke();
    }

    public bool DoMove(Vector2 movement)//tests horizontalMovement
    {
        if (movement == Vector2.zero) return false;
        RaycastHit2D hit;
        if (movement.x > 0)
        {
            hit = TerrainCheckRight();
        }
        else
        {
            hit = TerrainCheckLeft();
        }
        if (hit)
        {
            movement *= hit.fraction;
        }

        
        transform.Translate(movement);
        return true;
    }

    RaycastHit2D TerrainCheckRight()
    {
        Vector2 playerSize = MathTools.Vector3Multiply(transform.localScale, this.GetComponent<BoxCollider2D>().size);
        DebugExtension.DebugBounds(new(transform.position + Vector3.right * .1f, playerSize),
            Color.magenta);
        return Physics2D.BoxCast(transform.position, playerSize, 0, Vector3.right, .1f, ~LayerMask.GetMask("Player"));
    }

    RaycastHit2D TerrainCheckLeft()
    {
        Vector2 playerSize = MathTools.Vector3Multiply(transform.localScale, this.GetComponent<BoxCollider2D>().size);
        DebugExtension.DebugBounds(new(transform.position + Vector3.left * .1f, playerSize),
            Color.magenta);
        return Physics2D.BoxCast(transform.position, playerSize, 0, Vector3.left, .1f, ~LayerMask.GetMask("Player"));
    }

    public bool IsAgainstWall()
    {
        if (TerrainCheckLeft().collider != null) return true;
        if (TerrainCheckRight().collider != null) return true;
        return false;
    }
    void WallJump(Vector3 direction)
    {
        _rigidbody.AddForce(jumpForce * wallJumpIntensity * direction.normalized);
        _rigidbody.AddForce(jumpForce * wallJumpIntensity * Vector3.up);
        onJump.Invoke();
    }

    public void Death()
    {
        transform.position = spawnPoint;
        Debug.Log("You died lmao");
    }
}
