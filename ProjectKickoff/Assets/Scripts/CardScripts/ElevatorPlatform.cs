using Unity.Mathematics;
using UnityEngine;

public class ElevatorPlatform : CardBase
{
    Vector3 startPos;
    public float movementRange = 2;
    public float speed = 1;
    protected override void StartEffect()
    {
        startPos = transform.position;
    }
    protected override void UpdateEffect()
    {
        transform.position = startPos + new Vector3(0, math.sin(Time.timeSinceLevelLoad * speed), 0) * movementRange;
    }
}
