using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> currentCards = new();
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
        for (int i = 0; i < currentCards.Count; i++)
        {
            FoldoutCard card = Instantiate(cardElement, cardUI.transform);
            card.GetComponent<Image>().sprite = currentCards[i].GetComponent<SpriteRenderer>().sprite;
            card.index = i;
            cardUI.currentCards.Add(card.gameObject);
        }
        cardUI.SetCardPositions();
    }

    public void AddCard(GameObject cardToAdd)
    {
        currentCards.Add(cardToAdd);
        UpdateCards();
    }
    public void RemoveCard(GameObject cardToRemove)
    {
        currentCards.Remove(cardToRemove);
        UpdateCards();
    }
}
