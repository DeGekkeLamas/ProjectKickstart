using UnityEngine;
using UnityEngine.Events;

public class CoinCollect : MonoBehaviour
{
    public int value = 1;
    public AudioClip onPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.UpdateCoinCount(GameManager.instance.collectedCoins + value);
        AudioPlayer.Play(onPickup);
        Destroy(gameObject);
    }
}
