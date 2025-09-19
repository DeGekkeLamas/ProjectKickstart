using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CardPlacer : MonoBehaviour
{
    public Color spotAvailableColor = Color.green;
    public Color spotUnavailableColor = Color.red;

    public CardBase card;
    public static GameObject cardContainer;
    new SpriteRenderer renderer;
    private Vector3 cardPosition;
    private float cardRotation;
    private bool cardPosSet;

    private void Start()
    {
        if (cardContainer == null) cardContainer = new GameObject("CardContainer");
        CardBase cardObject = Instantiate(card, this.transform.position, Quaternion.identity, this.transform);
        // Destroy physical crd stuff
        Destroy(cardObject.GetComponent<Collider2D>());
        Destroy(cardObject.GetComponent<CardBase>());
        if (cardObject.TryGetComponent(out SpriteRenderer renderer))
        {
            this.renderer = renderer;
        }
        else
        {
            this.renderer = cardObject.AddComponent<SpriteRenderer>();
            this.renderer.sprite = cardObject.platformSprite;
            this.renderer.sortingOrder = 5;
        }

        // Destroy children
        for(int i = cardObject.transform.childCount; i > 0; i-- )
        {
            Destroy(cardObject.transform.GetChild(i-1).gameObject);
        }

        cardRotation = 0;
        cardPosition = Vector3.zero;
        cardPosSet = false;
    }

    void Update()
    {
        // Set position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        if (!cardPosSet) transform.position = mouseWorldPos;
        

        // Set rotation
        if (cardPosSet)
        {
            if (Vector3.Distance(cardPosition, mouseWorldPos) > 1f)
            {
                Vector2 delta = (Vector2)mouseWorldPos - (Vector2)cardPosition;
                float angleRad = Mathf.Atan2(delta.y, delta.x);
                cardRotation = angleRad * Mathf.Rad2Deg;
            }
            else cardRotation = 0;
        }
        transform.rotation = Quaternion.AngleAxis(cardRotation, Vector3.forward);

        // Check if valid spot on click
        Bounds objectBounds = this.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds;
        RaycastHit2D pointOccupied = Physics2D.BoxCast(objectBounds.center, objectBounds.size, cardRotation, Vector2.zero);
        bool hitSomething = pointOccupied.collider != null;
        DebugExtension.DebugBounds(objectBounds, hitSomething ? Color.green : Color.red);
        renderer.color = hitSomething ? spotUnavailableColor : spotAvailableColor;

        if (Input.GetMouseButtonDown(0) && !cardPosSet)
        {
            if (hitSomething)
            {
                DebugExtension.DebugArrow(objectBounds.center, pointOccupied.point - (Vector2)objectBounds.center, Color.red, 1);
            }
            else // Save position
            {
                cardPosition = objectBounds.center;
                cardPosSet = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (cardPosSet)
            {
                if (hitSomething)
                {
                    cardRotation = 0;
                    cardPosition = Vector3.zero;
                    cardPosSet = false;
                }
                else PlaceCard();
            }
        }
        if (Input.GetMouseButtonDown(1) && cardPosSet)
        {
            cardRotation = 0;
            cardPosition = Vector3.zero;
            cardPosSet = false;
        }
    }

    private void PlaceCard()
    {
        
        CardBase placedCard = Instantiate(card, cardPosition, transform.rotation, cardContainer.transform);
        placedCard.originalPrefab = card;
        FoldoutCard.isCurrentlyPlacingCard = false;
        Debug.Log("Placed card");
        cardPosition = Vector3.zero;
        cardPosSet = false;
        GameplayLoopManager.instance.cardPlacers.Remove(this);
        Destroy(this.gameObject);
    }
}
