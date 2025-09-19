using UnityEngine;
using System.Collections.Generic;

public class BackgroundPlayerFollower : MonoBehaviour
{
    public float followSpeed = .5f;
    public Vector3 spriteDimensions;
    GameObject originalSprite;

    Dictionary<Vector3, GameObject> BGPiecesSpawned = new();

    private void Awake()
    {
        originalSprite = transform.GetChild(0).gameObject;
        spriteDimensions = originalSprite.GetComponent<SpriteRenderer>().bounds.size;
        Instantiate(originalSprite, originalSprite.transform.position + spriteDimensions, Quaternion.identity, this.transform);
    }
    void Update()
    {
        transform.position = Camera.main.transform.position * followSpeed;

        Bounds camBounds = new( Camera.main.transform.position, 
            new Vector3(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize) * 2 );
        DebugExtension.DebugBounds(camBounds, Color.magenta);

        Vector3 pos = transform.position;
        for(int x = 0; x < RoundToNearestSpritePos(camBounds.size + pos).x - pos.x / spriteDimensions.x; x++)
        {
            for (int y = 0; y < RoundToNearestSpritePos(camBounds.size + pos).y - pos.y / spriteDimensions.y; y++)
            {
                SpawnSprite( RoundToNearestSpritePos( new(camBounds.min.x + x * spriteDimensions.x, 
                    camBounds.min.y + y * spriteDimensions.y
                    , 0) ) );
            }
        }
        //Debug.Log($"{camBounds.size.x / spriteDimensions.x}, {camBounds.size.y / spriteDimensions.y}");
    }

    void SpawnSprite(Vector3 position)
    {
        if (BGPiecesSpawned.ContainsKey(position))
        {
            return;
        }
        BGPiecesSpawned[position] = Instantiate(originalSprite, position, Quaternion.identity, this.transform);
    }

    Vector3 RoundToNearestSpritePos(Vector3 input)
    {
        input -= this.transform.position;
        // Rounding
        input = MathTools.Vector3Divide(input, spriteDimensions);
        input = MathTools.Vector3Round(input);
        input = MathTools.Vector3Multiply(input, spriteDimensions);

        return input;
    }
}
