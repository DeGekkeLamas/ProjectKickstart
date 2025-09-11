using Microsoft.Unity.VisualStudio.Editor;
using NaughtyAttributes;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPickerManager : MonoBehaviour
{
    public GameObject CardUIPickerPrefab;
    public List<GameObject> allCardPrefabs = new();
    public int cardsCount;
    public List<Vector2> positions = new();
    public List<GameObject> selectedCards = new();
    public List<GameObject> collectdCards = new();
    public GameManager gameManager;
    

    private GameObject GenerateRandomCard()
    {
        return allCardPrefabs[Random.Range(0, allCardPrefabs.Count)];
    }

    [Button]
    public void SpawnCards()
    {
        ClearCards();
        for (int i = 0; i < cardsCount; i++)
        {
            GameObject prefabreference = GenerateRandomCard();
            GameObject cardObject = Instantiate(prefabreference, positions[i], Quaternion.identity, transform);
            GameObject cardSelectionObject = Instantiate(CardUIPickerPrefab, positions[i], Quaternion.identity, cardObject.transform);
            CardPickerUI pickerUIScript = cardSelectionObject.GetComponent<CardPickerUI>();
            pickerUIScript.manager = this;
            pickerUIScript.prefabReference = prefabreference;
            cardObject.GetComponent<BoxCollider2D>().enabled = false;
            CardBase cardBase = cardObject.GetComponent<CardBase>();
            if (cardBase) cardBase.enabled = false;
            Rigidbody2D rigidBody = cardObject.GetComponent<Rigidbody2D>();
            if (rigidBody) DestroyImmediate(rigidBody);
            selectedCards.Add(cardObject);
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

    public void CollectCard(GameObject instance, GameObject prefabReference)
    {
        instance.gameObject.SetActive(false);
        
        gameManager.AddCard(prefabReference);
    }
}
