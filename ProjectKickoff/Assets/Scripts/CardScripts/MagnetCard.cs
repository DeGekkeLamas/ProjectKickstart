using System.ComponentModel;
using UnityEngine;

public class MagnetCard : CardBase
{
    public float gravity;

    protected override void UpdateEffect()
    {
        if (PlayerController.instance == null) return;

        Vector2 playerPos = PlayerController.instance.transform.position;
        Vector2 platformPos = transform.position;
        Vector2 diff = platformPos - playerPos;
        float distance = diff.magnitude;
        Vector2 attraction = diff.normalized / (distance * distance) * gravity;
        PlayerController.instance.DoMove(attraction * Time.deltaTime);
    }
}
