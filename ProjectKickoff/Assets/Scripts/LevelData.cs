using System.Collections;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public RectInt levelBounds;
    public bool displayLevelbounds;

    public static LevelData instance;

    private void Awake()
    {
        instance = this;
    }

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
}
