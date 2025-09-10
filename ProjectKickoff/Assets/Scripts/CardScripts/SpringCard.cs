using System.ComponentModel;
using UnityEngine;

public class SpringCard : CardBase
{
    
    protected override void EnterEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        playerScript.jumpForce *= 2;
    }
    protected override void ExitEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        playerScript.jumpForce *= 0.5f;
    }
}
