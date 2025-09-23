using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class CardHandLayout : MonoBehaviour
{
    public Vector2 rotationRange;
    public Vector2 PlacementRange;
    public float curveHeight = 10;

    public List<GameObject> currentCards = new();

    private void OnValidate()
    {
        // Ensure max value isnt less than min value
        rotationRange = new(rotationRange.x, Mathf.Max(rotationRange.x, rotationRange.y));
        PlacementRange = new(PlacementRange.x, Mathf.Max(PlacementRange.x, PlacementRange.y));
    }

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
            float inverseI = 1f/Mathf.Max(1,currentCards.Count-1) * i;
            GameObject baseUIElement = currentCards[i];//this is the cardUIBaseObject
            GameObject colliderUIElement = baseUIElement.transform.GetChild(0).gameObject;//this is the collider UIElement
            GameObject displayUIElement = baseUIElement.transform.GetChild(1).gameObject;//this is the display UIElement

            colliderUIElement.transform.eulerAngles = new(0, 0, Mathf.Lerp(rotationRange.x,rotationRange.y, inverseI));
            displayUIElement.transform.eulerAngles = new(0, 0, Mathf.Lerp(rotationRange.x, rotationRange.y, inverseI));
            baseUIElement.GetComponentInChildren<FoldoutCard>().originalRotation = colliderUIElement.transform.localEulerAngles;
            colliderUIElement.transform.position = this.transform.position + 
                new Vector3(Mathf.Lerp(PlacementRange.x, PlacementRange.y, 1-inverseI), 
                Mathf.Sin(inverseI * Mathf.PI) * curveHeight, 0) * canvasScale;
            displayUIElement.transform.position = this.transform.position +
                new Vector3(Mathf.Lerp(PlacementRange.x, PlacementRange.y, 1 - inverseI),
                Mathf.Sin(inverseI * Mathf.PI) * curveHeight, 0) * canvasScale;
            displayUIElement.GetComponent<Canvas>().sortingOrder = 0;

            // Color used for old debug, no longer needed mostly
            //currentCards[i].GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, inverseI);

            // Correct hierarchy
            CorrectHierarchy();
        }
    }

    /// <summary>
    /// Corrects the hierarchy order so cards are shown over each other correctly
    /// </summary>
    public void CorrectHierarchy()
    {
        for (int i = 0; i < currentCards.Count; i++)
        {
            currentCards[i].transform.parent.SetSiblingIndex(0);
        }
    }

    public void SwitchVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
