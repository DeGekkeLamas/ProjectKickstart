using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardPickerUI : MonoBehaviour
{
    public CardPickerManager manager;
    public CardBase prefabReference;
    public TMP_Text textMeshPro;
    Image thisImage;


    [Button]
    public void Initiate()
    {
        thisImage = GetComponent<Image>();
        textMeshPro = GetComponentInChildren<TMP_Text>();
        manager = CardPickerManager.instance;
        prefabReference = manager.currentCardbase;
        thisImage.sprite = prefabReference.cardSprite;
        textMeshPro.text = prefabReference.cardText;
    }
    public void CollectCard()
    {
        print("collectingCard");
        if (CardPickerManager.instance.maxCardsToPick > CardPickerManager.instance.cardsPicked)
        {
            manager.CollectCard(gameObject, prefabReference);
            CardPickerManager.instance.cardsPicked++;
            CardPickerManager.instance.UpdateCardCounter();
            AudioPlayer.Play(AudioPlayer.instance.placementSound);
        }
        else
        {
            Debug.Log("Hand is full already");
        }
    }
}
