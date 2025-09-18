using System.Collections;
using UnityEngine;

public class FragileCard : CardBase
{
    public Collider2D thisCardsCollider;
    protected override void ExitEffect(Collision2D collision)
    {
        StartCoroutine(BreakCard());
    }

    /// <summary>
    /// For if we want to animate it later
    /// </summary>
    IEnumerator BreakCard()
    {
        yield return null;
        Destroy(this.gameObject);
    }
}
