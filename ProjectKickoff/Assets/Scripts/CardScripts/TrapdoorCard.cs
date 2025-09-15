using System.Collections;
using UnityEngine;

public class TrapdoorCard : CardBase
{
    Collider2D thisCardsCollider;
    public float lifeSpan = 2;
    public float durationLeft;
    private void Awake()
    {
        thisCardsCollider = GetComponent<Collider2D>();
    }
    protected override void EnterEffect(Collision2D collision)
    {
        durationLeft = lifeSpan;
    }
    
    protected override void StayEffect(Collision2D collision)
    {
        durationLeft -= Time.deltaTime;
        if (durationLeft < 0) thisCardsCollider.enabled = false;
    }
    protected override void ExitEffect(Collision2D collision)
    {
        StartCoroutine(ResetTrapdoor());
    }

    IEnumerator ResetTrapdoor()
    {
        yield return new WaitForSeconds(1);
        thisCardsCollider.enabled = true;
    }
}
