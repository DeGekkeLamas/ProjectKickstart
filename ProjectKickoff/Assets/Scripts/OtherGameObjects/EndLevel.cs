using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CompleteGame();
    }

    public void CompleteGame()
    {
        GameplayLoopManager.instance.SetState((int)GameState.startGame);
    }
}
