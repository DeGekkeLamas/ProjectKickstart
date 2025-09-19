using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPickerManager : MonoBehaviour
{
    public static CardPickerManager instance;
    public CardPickerManager Instance;
    public GameObject CardUIPickerPrefab;
    public List<CardBase> allCardPrefabs = new();
    public int cardsCount;
    public List<Vector2> positions = new();
    public List<GameObject> selectedCards = new();
    public CardBase currentCardbase;

    [Button]
    void Awake()
    {
        instance = this;
        Instance = instance;
    }

    private CardBase GenerateRandomCard()
    {
        return allCardPrefabs[Random.Range(0, allCardPrefabs.Count)];
    }

    [Button]
    public void SpawnCards()
    {
        if (Application.isPlaying && GameManager.instance.collectedCoins < 1) return;
        if (Application.isPlaying) GameManager.instance.UpdateCoinCount(GameManager.instance.collectedCoins - 1);
        ClearCards();
        for (int i = 0; i < cardsCount; i++)
        {
            currentCardbase = GenerateRandomCard();
            GameObject cardSelectionObject = Instantiate(CardUIPickerPrefab, positions[i], Quaternion.identity, transform);
            cardSelectionObject.transform.localScale = Vector3.one * 200;
            CardPickerUI pickerUIScript = cardSelectionObject.GetComponent<CardPickerUI>();
            pickerUIScript.Initiate();

            selectedCards.Add(cardSelectionObject);
        }
    }

    [Button]
    public void ClearCards()
    {
        foreach (var item in selectedCards)
        {
            DestroyImmediate(item);
        }
        selectedCards.Clear();
    }

    public void CollectCard(GameObject instance, CardBase prefabReference)
    {
        Destroy(instance);
        GameManager.instance.AddCardToDeck(prefabReference);
    }
}
