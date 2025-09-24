using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
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
    public int maxCardsToPick = 6;
    public AudioClip cardFoldNoise;
    [HideInInspector] public int cardsPicked;
    public TMP_Text cardsPickedCounter;
    public Canvas theCanvas;

    [Button]
    void Awake()
    {
        instance = this;
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
        AudioPlayer.Play(cardFoldNoise);
        for (int i = 0; i < cardsCount; i++)
        {
            currentCardbase = GenerateRandomCard();
            GameObject cardSelectionObject = Instantiate(CardUIPickerPrefab, theCanvas.pixelRect.center + positions[i] * theCanvas.pixelRect.width/3.5f, Quaternion.identity, transform);
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

    public void UpdateCardCounter()
    {
        cardsPickedCounter.text = $"Cards picked: {cardsPicked} / {maxCardsToPick}";
    }
}
