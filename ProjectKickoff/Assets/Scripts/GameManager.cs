using System.Collections.Generic;
using UnityEngine;

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
        while (cardUI.transform.childCount > 0)
        {
            Destroy(cardUI.transform.GetChild(0));
        }
        for (int i = 0; i < currentCards.Count; i++)
        {
            Instantiate(cardElement, cardUI.transform);
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
