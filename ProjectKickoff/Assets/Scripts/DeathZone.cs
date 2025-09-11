using UnityEngine;

/// <summary>
/// Should be placed on any object that should kill the player
/// </summary>
public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision");
        if (collision.collider.gameObject.TryGetComponent(out PlayerController player)) player.Death();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger");
        if (other.gameObject.TryGetComponent(out PlayerController player)) player.Death();
    }
}
