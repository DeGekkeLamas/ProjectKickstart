using UnityEngine;
using System.Collections.Generic;

public class BackgroundPlayerFollower : MonoBehaviour
{
    public float followSpeed = .5f;
    Vector3 spriteDimensions;
    public GameObject originalSprite;

    Dictionary<Vector3, GameObject> BGPiecesSpawned = new();

    private void Start()
    {
        spriteDimensions = originalSprite.GetComponent<SpriteRenderer>().bounds.size;

        // Spawn bg pieces
        for(int x = 0; x < LevelData.instance.levelBounds.size.x * 1f / spriteDimensions.x; x++)
        {
            for (int y = 0; y < LevelData.instance.levelBounds.size.y * 1f / spriteDimensions.y; y++)
            {
                Instantiate(originalSprite, new(x * spriteDimensions.x, 
                    y * spriteDimensions.y, 0), Quaternion.identity, this.transform);
            }
        }
    }
    void LateUpdate()
    {
        transform.position = Camera.main.transform.position * followSpeed;
    }

    void SpawnSprite(Vector3 position)
    {
        if (BGPiecesSpawned.ContainsKey(position))
        {
            return;
        }
        Debug.Log("Spawned wallpiece");
        BGPiecesSpawned[position] = Instantiate(originalSprite, position + 
            MathTools.Vector3Modulo(this.transform.position, spriteDimensions), Quaternion.identity, this.transform);
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
