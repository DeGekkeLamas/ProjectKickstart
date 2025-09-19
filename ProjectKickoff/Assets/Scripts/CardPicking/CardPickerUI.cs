using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;


public class CardPickerUI : MonoBehaviour
{
    public CardPickerManager manager;
    public CardBase prefabReference;
    Image thisImage;


    [Button]
    public void Initiate()
    {
        thisImage = GetComponent<Image>();
        manager = CardPickerManager.instance;
        prefabReference = manager.currentCardbase;
        thisImage.sprite = prefabReference.cardSprite;
    }
    public void CollectCard()
    {
        print("collectingCard");
        if (CardPickerManager.instance.maxCardsToPick > CardPickerManager.instance.cardsPicked)
        {
            manager.CollectCard(gameObject, prefabReference);
            CardPickerManager.instance.cardsPicked++;
        }
        else
        {
            Debug.Log("Hand is full already");
        }
    }
}
