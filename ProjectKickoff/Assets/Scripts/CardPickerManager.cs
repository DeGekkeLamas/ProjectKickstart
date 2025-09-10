using Microsoft.Unity.VisualStudio.Editor;
using NaughtyAttributes;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardPickerManager : MonoBehaviour
{
    public GameObject CardUIPickerPrefab;
    public List<GameObject> allCardPrefabs = new();
    public int cardsCount;
    public List<Vector2> positions = new();
    public List<GameObject> selectedCards = new();
    

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
            selectedCards.Add(Instantiate(CardUIPickerPrefab, positions[i], Quaternion.identity, transform));
            selectedCards.Add(Instantiate(GenerateRandomCard(), positions[i], Quaternion.identity, transform));
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
}
