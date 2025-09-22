using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class DashPlatform : CardBase
{
    public Vector3 dashDirection;

    private void Awake()
    {
        MovingObject move = this.GetComponent<MovingObject>();
        move.moveRange = dashDirection.normalized / 10f;
    }
    protected override void EnterEffect(Collision2D collision)
    {
        Rigidbody2D playerRigidBody = collision.collider.gameObject.GetComponent<Rigidbody2D>();
        Vector3 usedRotation= VectorMath.RotateVectorXY(dashDirection, transform.rotation.eulerAngles.z);
        playerRigidBody.AddForce(usedRotation);

    }
}
