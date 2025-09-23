using UnityEngine;
using NaughtyAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System;

/// <summary>
/// this is a base class for card effects, also place this on cards without script based effects, at this is used for their card data too
/// </summary>
public class CardBase : MonoBehaviour
{
    [HideInInspector] public CardBase originalPrefab;
    [InfoBox("The sprite on this script is used for the UI card in hand, the sprite on the prefabs sprite renderer is used for the placed platform")]

    [Tooltip("The sprite used for the card in hand UI")]
    public Sprite cardSprite;
    [InfoBox("platformSprite is only needed if the root gameobject with this has no spriterenderer, otherwise leave this empty")]
    public Sprite platformSprite;
    [TextArea] public string cardText;
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
        AudioPlayer.Play(AudioPlayer.instance.placementSound);
        Destroy(this.gameObject);
    }

    public bool ListContainsMatchingType(List<CardBase> cardList, CardBase CardType, out CardBase match)
    {
        foreach (var item in cardList)
        {
            if (item.GetType() == CardType.GetType())
            {
                match = item;
                return true;
            }
        }
        print("no match");
        match = null;
        return false;
    }

    
}
