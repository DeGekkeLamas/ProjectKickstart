using UnityEngine;
using System.Collections.Generic;

public class SpawnpointPlatform : CardBase
{
    public Sprite ActiveSprite;
    static List<SpawnpointPlatform> allSpawnpoints = new();
    private void Awake()
    {
        allSpawnpoints.Add(this);
    }

    private void OnDestroy()
    {
        allSpawnpoints.Remove(this);
    }

    protected override void EnterEffect(Collision2D collision)
    {
        SetCheckpointSprite();
        PlayerController.instance.spawnPoint = this.transform.position + this.transform.up * 
            PlayerController.instance.transform.lossyScale.y;
            print("spawnpoint set");
    }

    void SetCheckpointSprite()
    {
        foreach (var spawnpoint in allSpawnpoints)
        {
            spawnpoint.GetComponent<SpriteRenderer>().sprite = platformSprite;
        }
        this.GetComponent<SpriteRenderer>().sprite = ActiveSprite;
    }
}
