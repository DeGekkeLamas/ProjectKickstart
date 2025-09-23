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
    Bounds objectBounds;

    // These are used for displaying the bounds of moving platforms
    Vector3 moveBoundsMin;
    Vector3 moveBoundsMax;
    GameObject moveDisplayMin;
    GameObject moveDisplayMax;
    bool isMovingPlatform;

    private void Start()
    {
        if (cardContainer == null) cardContainer = new GameObject("CardContainer");
        CardBase cardObject = Instantiate(card, this.transform.position, Quaternion.identity, this.transform);
        // Set object bounds
        objectBounds = cardObject.transform.GetComponent<Collider2D>().bounds;

        // Destroy physical card stuff
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

        // increase bounds for cards that move
        if (card.TryGetComponent(out MovingPlatform moving))
        {
            objectBounds.size = new(objectBounds.size.x + 2 * moving.movementRange, objectBounds.size.y);
            moveBoundsMin = new(-moving.movementRange, 0);
            moveBoundsMax = new(moving.movementRange, 0);
            PlaceMoveDisplays();
        }
        if (card.TryGetComponent(out ElevatorPlatform elevator))
        {
            objectBounds.size = new(objectBounds.size.x, objectBounds.size.y + 2 * elevator.movementRange);
            moveBoundsMin = new(0, -elevator.movementRange);
            moveBoundsMax = new(0, elevator.movementRange);
            PlaceMoveDisplays();
        }

        cardRotation = 0;
        cardPosition = Vector3.zero;
        cardPosSet = false;
    }

    void PlaceMoveDisplays()
    {
        isMovingPlatform = true;
        moveDisplayMin = new("MoveDisplayMin");
        moveDisplayMax = new("MoveDisplayMax");
        moveDisplayMin.transform.parent = this.transform;
        moveDisplayMax.transform.parent = this.transform;
        SpriteRenderer minSprite = moveDisplayMin.AddComponent<SpriteRenderer>();
        SpriteRenderer maxSprite = moveDisplayMax.AddComponent<SpriteRenderer>();
        minSprite.sprite = this.renderer.sprite;
        maxSprite.sprite = this.renderer.sprite;
        minSprite.renderingLayerMask = renderer.renderingLayerMask;
        maxSprite.renderingLayerMask = renderer.renderingLayerMask;
        minSprite.color = new(1,1,1,0.5f);
        maxSprite.color = new(1,1,1,0.5f);
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
        RaycastHit2D pointOccupied = Physics2D.BoxCast(objectBounds.center, objectBounds.size, cardRotation, Vector2.zero);
        bool hitSomething = pointOccupied.collider != null;

        // Visual effect
        objectBounds.center = this.transform.position;
        DebugExtension.DebugBounds(objectBounds, hitSomething ? Color.red : Color.green);
        renderer.color = hitSomething ? spotUnavailableColor : spotAvailableColor;
        // moving platform displays
        if (isMovingPlatform)
        {
            moveDisplayMin.transform.position = this.transform.position + moveBoundsMin;
            moveDisplayMax.transform.position = this.transform.position + moveBoundsMax;
        }

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
        AudioPlayer.Play(AudioPlayer.instance.placementSound);
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
