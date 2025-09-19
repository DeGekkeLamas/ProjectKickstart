using System.ComponentModel;
using UnityEngine;

public class SpringCard : CardBase
{
    public float mulitplier = 2;
    
    protected override void EnterEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        playerScript.jumpForce *= mulitplier;
    }
    protected override void ExitEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        playerScript.jumpForce *= 1f/mulitplier;
    }
}
