using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : CardBase
{
    Vector3 startPos;
    protected override void StartEffect()
    {
        startPos = transform.position;
    }
    protected override void UpdateEffect()
    {
        transform.position = startPos + new Vector3(math.sin(Time.timeSinceLevelLoad), 0, 0);
    }
}
