using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.UpdateCoinCount(GameManager.instance.collectedCoins + 1);
        Destroy(gameObject);
    }
}
