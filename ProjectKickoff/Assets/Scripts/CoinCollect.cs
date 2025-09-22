using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public int value = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.UpdateCoinCount(GameManager.instance.collectedCoins + value);
        Destroy(gameObject);
    }
}
