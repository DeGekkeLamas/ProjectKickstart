using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPickerManager : MonoBehaviour
{
    public GameObject CardUIPickerPrefab;
    public List<CardBase> allCardPrefabs = new();
    public int cardsCount;
    public List<Vector2> positions = new();
    public List<GameObject> selectedCards = new();
    
    

    private CardBase GenerateRandomCard()
    {
        return allCardPrefabs[Random.Range(0, allCardPrefabs.Count)];
    }

    [Button]
    public void SpawnCards()
    {
        ClearCards();
        for (int i = 0; i < cardsCount; i++)
        {
            CardBase prefabreference = GenerateRandomCard();
            CardBase cardObject = Instantiate(prefabreference, positions[i], Quaternion.identity, transform);
            Sprite usedSprite = cardObject.GetComponent<SpriteRenderer>().sprite;
            DestroyImmediate(cardObject);
            GameObject cardSelectionObject = Instantiate(CardUIPickerPrefab, positions[i], Quaternion.identity, transform);
            cardSelectionObject.transform.localScale = Vector3.one * 200;
            cardSelectionObject.GetComponent<UnityEngine.UI.Image>().sprite = usedSprite;
            CardPickerUI pickerUIScript = cardSelectionObject.GetComponent<CardPickerUI>();
            pickerUIScript.manager = this;
            pickerUIScript.prefabReference = prefabreference;

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
