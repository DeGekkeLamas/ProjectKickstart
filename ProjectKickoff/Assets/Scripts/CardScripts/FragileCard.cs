using UnityEngine;

public class FragileCard : CardBase
{
    public Collider2D thisCardsCollider;
    protected override void ExitEffect(Collision2D collision)
    {
        thisCardsCollider.enabled = false;
    }
}
