using System.ComponentModel;
using UnityEngine;

public class MagnetCard : CardBase
{
    PlayerController player;
    public float gravity;
    protected override void StartEffect()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    protected override void UpdateEffect()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 platformPos = transform.position;
        Vector2 diff = platformPos - playerPos;
        float distance = diff.magnitude;
        Vector2 attraction = diff.normalized / (distance * distance) * gravity;
        player.DoMove(attraction * Time.deltaTime);
    }
}
