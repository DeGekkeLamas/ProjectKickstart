using UnityEngine;

public class SpawnpointPlatform : CardBase
{
    public static Vector3 currentCheckpoint;
    public Vector3 displayCheckpoint;

    protected override void StartEffect()
    {
        currentCheckpoint = FindAnyObjectByType<PlayerController>().transform.position;
    }
    protected override void StayEffect(Collision2D collision)
    {
        print("spawnpoint trying to set");
        
        if (collision.collider.gameObject.name == "Player")
        {
            currentCheckpoint = collision.collider.transform.position;
            print("spawnpoint set");
        }
    }
    protected override void UpdateEffect()
    {
        displayCheckpoint = currentCheckpoint;
    }
}
