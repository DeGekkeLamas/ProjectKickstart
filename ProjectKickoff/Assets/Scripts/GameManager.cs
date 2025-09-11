using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cardsInHand = new();
    public List<GameObject> cardsInDeck = new();
    public static GameManager instance;

    public CardHandLayout cardUI;
    public FoldoutCard cardElement;

    void Awake()
    {
        instance = this;
        UpdateCards();
    }

    void UpdateCards()
    {
        for(int i = cardUI.transform.childCount; i > 0; i--)
        {
            Destroy(cardUI.transform.GetChild(i-1).gameObject);
        }
        cardUI.currentCards.Clear();
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            FoldoutCard card = Instantiate(cardElement, cardUI.transform);
            card.GetComponent<Image>().sprite = cardsInHand[i].GetComponent<SpriteRenderer>().sprite;
            card.index = i;
            cardUI.currentCards.Add(card.gameObject);
        }
        cardUI.SetCardPositions();
    }

    public void AddCardToHand(GameObject cardToAdd)
    {
        cardsInHand.Add(cardToAdd);
        UpdateCards();
    }
    public void RemoveCardFromHand(GameObject cardToRemove)
    {
        cardsInHand.Remove(cardToRemove);
        UpdateCards();
    }
    public void AddCardToDeck(GameObject cardToAdd)
    {
        cardsInDeck.Add(cardToAdd);
    }
    public void RemoveCardFromDeck(GameObject cardToAdd)
    {
        cardsInDeck.Remove(cardToAdd);
    }


}
