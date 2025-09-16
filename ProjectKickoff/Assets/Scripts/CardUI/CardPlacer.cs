using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CardPlacer : MonoBehaviour
{
    public Color spotAvailableColor = Color.green;
    public Color spotUnavailableColor = Color.red;

    public CardBase card;
    public static GameObject cardContainer;
    new SpriteRenderer renderer;

    private void Start()
    {
        if (cardContainer == null) cardContainer = new GameObject("CardContainer");
        CardBase cardObject = Instantiate(card, this.transform.position, Quaternion.identity, this.transform);
        Destroy(cardObject.GetComponent<Collider2D>());
        Destroy(cardObject.GetComponent<CardBase>());
        renderer = cardObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Set position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        transform.position = mouseWorldPos;

        // Check if valid spot on click
        Bounds objectBounds = this.transform.GetChild(0).GetComponent<Renderer>().bounds;
        RaycastHit2D pointOccupied = Physics2D.BoxCast(objectBounds.center, objectBounds.size, 0, Vector2.zero);
        bool hitSomething = pointOccupied.collider != null;
        DebugExtension.DebugBounds(objectBounds, hitSomething ? Color.green : Color.red);
        renderer.color = hitSomething ? spotUnavailableColor : spotAvailableColor;

        if (Input.GetMouseButtonDown(0))
        {
            if (hitSomething)
            {
                DebugExtension.DebugArrow(objectBounds.center, pointOccupied.point - (Vector2)objectBounds.center, Color.red, 1);
            }
            else // Place card
            {
                Instantiate(card, objectBounds.center, Quaternion.identity, cardContainer.transform);
                FoldoutCard.isCurrentlyPlacingCard = false;
                Debug.Log("Placed card");
                Destroy(this.gameObject);
            }
        }
    }
}
