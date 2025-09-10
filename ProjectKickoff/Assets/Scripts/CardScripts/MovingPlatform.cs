using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : CardBase
{
    private Vector3 startPos;
    protected override void StartEffect()
    {
        startPos = transform.position;
    }
    protected override void UpdateEffect()
    {
        transform.position = startPos + new Vector3(math.sin(Time.timeSinceLevelLoad), 0, 0);
    }
    
    protected override void StayEffect(Collision2D collision)
    {
        PlayerController playerScript = collision.collider.gameObject.GetComponent<PlayerController>();
        Vector3 oldPos = startPos + new Vector3(math.sin(Time.timeSinceLevelLoad - Time.deltaTime), 0, 0);
        Vector3 currentPos = startPos + new Vector3(math.sin(Time.timeSinceLevelLoad), 0, 0);
        Vector3 diffPos = currentPos - oldPos;
        playerScript.DoMove(diffPos);
    }
}
