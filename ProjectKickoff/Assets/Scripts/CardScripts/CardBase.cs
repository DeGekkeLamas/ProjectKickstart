using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// this is a base class for card effects, also place this on cards without script based effects, at this is used for their card data too
/// </summary>
public class CardBase : MonoBehaviour
{
    [HideInInspector] public CardBase originalPrefab;
    [InfoBox("The sprite on this script is used for the UI card in hand, the sprite on the prefabs sprite renderer is used for the placed platform")]

    [Tooltip("The sprite used for the card in hand UI")]
    public Sprite cardSprite;

    private void Start() { StartEffect(); }
    private void Update() { UpdateEffect(); }

    private void OnCollisionEnter2D(Collision2D collision) { EnterEffect(collision); }
    
    private void OnCollisionExit2D(Collision2D collision) { ExitEffect(collision); }

    private void OnCollisionStay2D(Collision2D collision) { StayEffect(collision); }

    
    protected virtual void StartEffect() { }
    protected virtual void UpdateEffect() { }
    protected virtual void EnterEffect(Collision2D collision) { }
    protected virtual void ExitEffect(Collision2D collision) { }
    protected virtual void StayEffect(Collision2D collision) { }

    /// <summary>
    /// Remove card when clicked
    /// </summary>
    private void OnMouseDown()
    {
        if (GameplayLoopManager.instance.currentState != GameState.placingCards) return;

        GameManager.instance.AddCardToHand(originalPrefab);
        Destroy(this.gameObject);
    }
}
