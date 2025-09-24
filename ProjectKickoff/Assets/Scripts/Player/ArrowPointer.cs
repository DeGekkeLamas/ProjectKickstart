using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform target;
    public float offset = 3;

    void LateUpdate()
    {
        Vector3 difference = target.position - Camera.main.transform.position;
        Vector3 direction = difference.normalized;

        // Set position
        /// Position is clamped to camerabounds
        Bounds camBounds = new(Camera.main.transform.position - new Vector3(0,0,Camera.main.transform.position.z),
            new Vector3(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize) * 2);
        camBounds.extents -= (Vector3)Vector2.one * offset;

        this.transform.position = MathTools.Vector3Clamp(target.position, camBounds.center - camBounds.extents, 
            camBounds.center + camBounds.extents);

        // Slight offset when on top of goal
        Vector3 diffFromTarget = this.transform.position - target.position;
        if (diffFromTarget.magnitude < offset * offset) this.transform.position = target.position -
                offset * offset * MathTools.Vector3Abs(diffFromTarget).normalized;

        // Rotate towards target
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * direction;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        this.transform.rotation = targetRotation;

        // Debug
        DebugExtension.DebugArrow(Camera.main.transform.position, target.position - Camera.main.transform.position,
            diffFromTarget.magnitude < offset ? Color.red : Color.green);
    }
}
