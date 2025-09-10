using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class CardHandLayout : MonoBehaviour
{
    public Vector2 rotationRange;
    public Vector2 PlacementRange;

    public List<GameObject> currentCards = new();

    private void OnValidate()
    {
        // Ensure max value isnt less than min value
        rotationRange = new(rotationRange.x, Mathf.Max(rotationRange.x, rotationRange.y));
        PlacementRange = new(PlacementRange.x, Mathf.Max(PlacementRange.x, PlacementRange.y));
    }
    //private void Awake()
    //{
    //    SetCardPositions();
    //}

    [Button]
    public void SetCardPositions()
    {
        float canvasScale = transform.root.localScale.x;
        if (currentCards.Count < 1)
        {
            Debug.LogWarning("There are no cards in the hand");
            return;
        }
        for (int i = 0; i < currentCards.Count; i++)
        {
            // Place card
            float inverseI = (1f/currentCards.Count) * i;
            currentCards[i].transform.eulerAngles = new(0,0, Mathf.Lerp(rotationRange.x,rotationRange.y, inverseI));
            currentCards[i].transform.position = this.transform.position + new Vector3(Mathf.Lerp(PlacementRange.x, PlacementRange.y, 1-inverseI), 0, 0) / canvasScale;
            // Color used for old debug, no longer needed mostly
            //currentCards[i].GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, inverseI);

            // Correct hierarchy
            currentCards[i].transform.SetSiblingIndex(0);
        }
    }
}
