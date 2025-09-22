using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class DashPlatform : CardBase
{
    public Vector3 DashDirection;

    private void Awake()
    {
        MovingObject move = this.GetComponent<MovingObject>();
        move.moveRange = DashDirection.normalized / 10f;
    }
    protected override void EnterEffect(Collision2D collision)
    {
        Rigidbody2D playerRigidBody = collision.collider.gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody.AddForce(DashDirection, ForceMode2D.Force);

    }
}
