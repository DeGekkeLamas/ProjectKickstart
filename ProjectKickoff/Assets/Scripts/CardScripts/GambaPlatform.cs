using UnityEngine;

public class GambaPlatform : CardBase
{
    public CardBase[] possibleEffects;
    protected override void EnterEffect(Collision2D collision)
    {
        if (possibleEffects.Length < 1)
        {
            Debug.LogWarning("No possible effects set dumbass");
            return;
        }

        Instantiate(possibleEffects[ Random.Range(0, possibleEffects.Length) ], this.transform.position, 
            this.transform.rotation, this.transform.parent);
        Destroy(this.gameObject);
    }
}
