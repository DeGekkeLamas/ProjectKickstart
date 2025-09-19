using UnityEngine;
using System.Collections.Generic;

public class BackgroundPlayerFollower : MonoBehaviour
{
    public float followSpeed = .5f;
    Vector3 spriteDimensions;
    public GameObject originalSprite;

    Dictionary<Vector3, GameObject> BGPiecesSpawned = new();

    private void Awake()
    {
        spriteDimensions = originalSprite.GetComponent<SpriteRenderer>().bounds.size;
    }
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position * followSpeed;

        Bounds camBounds = new( Camera.main.transform.position, 
            new Vector3(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize) * 2 );
        DebugExtension.DebugBounds(camBounds, Color.magenta);

        Vector3 pos = transform.position;
        for(int x = 0; x < (RoundToNearestSpritePos(camBounds.size + pos).x - pos.x) / spriteDimensions.x + 1; x++)
        {
            for (int y = 0; y < (RoundToNearestSpritePos(camBounds.size + pos).y - pos.y) / spriteDimensions.y + 1; y++)
            {
                Vector3 spawnPos = RoundToNearestSpritePos(new(camBounds.min.x + x * spriteDimensions.x,
                    camBounds.min.y + y * spriteDimensions.y
                    , 0) );
                DebugExtension.DebugWireSphere(spawnPos, Color.blue, 1);
                SpawnSprite(spawnPos);
            }
        }
    }

    void SpawnSprite(Vector3 position)
    {
        if (BGPiecesSpawned.ContainsKey(position))
        {
            //Debug.Log("Did not spawn object");
            return;
        }
        BGPiecesSpawned[position] = Instantiate(originalSprite, position, Quaternion.identity, this.transform);
    }

    Vector3 RoundToNearestSpritePos(Vector3 input)
    {
        // Rounding
        input = MathTools.Vector3Divide(input, spriteDimensions);
        input = MathTools.Vector3Round(input);
        input = MathTools.Vector3Multiply(input, spriteDimensions);

        return input;
    }
}
