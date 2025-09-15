using UnityEngine;


public class CardPickerUI : MonoBehaviour
{
    public CardPickerManager manager;
    public CardBase prefabReference;

    public void CollectCard()
    {
        print("collectingCard");
        manager.CollectCard(gameObject, prefabReference);
    }
}
