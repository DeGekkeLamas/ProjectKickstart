using UnityEngine;

public class IceCard : CardBase
{
    Vector2 lastPos;
    protected override void EnterEffect(Collision2D collision)
    {
        lastPos = collision.collider.transform.position;
    }
    protected override void StayEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        Vector2 currentPos = collision.collider.transform.position;
        Vector2 diffPos = currentPos - lastPos;
        playerScript.DoMove(diffPos * 0.9f);
        
        //collision.collider.transform.Translate(diffPos * 0.9f);



        lastPos = currentPos;
    }

    
}
