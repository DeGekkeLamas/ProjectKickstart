using System.Collections;
using UnityEngine;

public class TrapdoorCard : CardBase
{
    public Collider2D thisCardsCollider;
    public float delay = 2;
    protected override void EnterEffect(Collision2D collision)
    {
        StartCoroutine(BreakPlatform());
    }

    IEnumerator BreakPlatform()
    {

        yield return new WaitForSeconds(delay);
        thisCardsCollider.enabled = false;
    }
}
