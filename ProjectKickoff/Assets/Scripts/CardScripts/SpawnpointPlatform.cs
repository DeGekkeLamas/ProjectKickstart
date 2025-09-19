using UnityEngine;
using System.Collections.Generic;

public class SpawnpointPlatform : CardBase
{
    public static Vector3 currentCheckpoint;
    public Vector3 displayCheckpoint;

    public Sprite ActiveSprite;
    static List<SpawnpointPlatform> allSpawnpoints = new();
    private void Awake()
    {
        allSpawnpoints.Add(this);
    }

    protected override void EnterEffect(Collision2D collision)
    {
        SetCheckpointSPrite();
        currentCheckpoint = PlayerController.instance.transform.position;
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

    void SetCheckpointSPrite()
    {
        foreach (var spawnpoint in allSpawnpoints)
        {
            spawnpoint.GetComponent<SpriteRenderer>().sprite = platformSprite;
        }
        this.GetComponent<SpriteRenderer>().sprite = ActiveSprite;
    }
}
