using System.Collections;
using UnityEngine;

public class PanningCamera : MonoBehaviour
{
    public RectInt levelBounds;
    public bool displayLevelbounds = true;

    public float moveSpeed = 5;
    public float SprintMulti = 2;

    private void OnValidate()
    {
        if (displayLevelbounds) StartCoroutine(ShowDebug());
    }

    IEnumerator ShowDebug()
    {
        while (displayLevelbounds)
        {
            DebugExtension.DebugBounds(new Bounds(levelBounds.center, (Vector3Int)levelBounds.size), Color.red);
            yield return null;
        }
    }

    void Update()
    {
        float moveSpeedUsed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift) 
            ? moveSpeed * SprintMulti : moveSpeed;

        transform.Translate(moveSpeedUsed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) );
        transform.position = new(Mathf.Clamp(transform.position.x, levelBounds.xMin, levelBounds.xMax)
            , Mathf.Clamp(transform.position.y, levelBounds.yMin, levelBounds.yMax), 0);
    }
}
