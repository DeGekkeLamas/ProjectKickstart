using Unity.Mathematics;
using UnityEngine;

public class DashPlatform : CardBase
{
    public Vector3 DashDirection;

    protected override void EnterEffect(Collision2D collision)
    {
        Rigidbody2D playerRigidBody = collision.collider.gameObject.GetComponent<Rigidbody2D>();
        playerRigidBody.AddForce(DashDirection, ForceMode2D.Force);

    }
}
