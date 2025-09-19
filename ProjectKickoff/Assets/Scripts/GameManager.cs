using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<CardBase> cardsInHand = new();
    public List<CardBase> cardsInDeck = new();
    public static GameManager instance;

    public CardHandLayout cardUI;
    public FoldoutCard cardElement;

    public int collectedCoins;
    public TMP_Text coinDisplay;
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
            card.GetComponent<Image>().sprite = cardsInHand[i].GetComponent<CardBase>().cardSprite;
            card.GetComponentInChildren<TMP_Text>().text = cardsInHand[i].GetComponent<CardBase>().cardText;
            card.index = i;
            cardUI.currentCards.Add(card.gameObject);
        }
        cardUI.SetCardPositions();
    }

    public void AddCardToHand(CardBase cardToAdd)
    {
        cardsInHand.Add(cardToAdd);
        UpdateCards();
    }
    public void RemoveCardFromHand(CardBase cardToRemove)
    {
        cardsInHand.Remove(cardToRemove);
        UpdateCards();
    }
    public void AddCardToDeck(CardBase cardToAdd)
    {
        cardsInDeck.Add(cardToAdd);
    }
    public void RemoveCardFromDeck(CardBase cardToAdd)
    {
        cardsInDeck.Remove(cardToAdd);
    }

    public void UpdateCoinCount(int newValue)
    {
        collectedCoins = newValue;
        coinDisplay.text = collectedCoins.ToString();
    }
}
