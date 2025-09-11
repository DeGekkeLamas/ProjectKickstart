using UnityEngine;


public class CardPickerUI : MonoBehaviour
{
    public CardPickerManager manager;
    public GameObject prefabReference;

    public void OnMouseDown()
    {
        print("collectingCard");
        manager.CollectCard(transform.parent.gameObject, prefabReference);
    }
}
