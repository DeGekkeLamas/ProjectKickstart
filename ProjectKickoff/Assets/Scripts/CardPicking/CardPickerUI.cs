using UnityEngine;
using UnityEngine.UI;


public class CardPickerUI : MonoBehaviour
{
    public CardPickerManager manager;
    public CardBase prefabReference;
    Image thisImage;

    private void Awake()
    {
        thisImage = GetComponent<Image>();
        thisImage.sprite = prefabReference.cardSprite;
    }
    public void CollectCard()
    {
        print("collectingCard");
        manager.CollectCard(gameObject, prefabReference);
    }
}
