using UnityEngine;

public class EndLevel : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameplayLoopManager.instance.SetState((int)GameplayLoopManager.GameState.choosingCards);
    }
}
