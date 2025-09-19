using System.Collections;
using UnityEngine;

public class TrapdoorCard : CardBase
{
    Collider2D thisCardsCollider;
    public float lifeSpan = 2;
    public float durationLeft;

    public Transform pivotR;
    public Transform pivotL;
    public float animationSpeed = 1;
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
        if (durationLeft < 0)
        {
            thisCardsCollider.enabled = false;
            StartCoroutine(OpenAnimation());
        }
    }
    protected override void ExitEffect(Collision2D collision)
    {
        StartCoroutine(ResetTrapdoor());
    }

    IEnumerator ResetTrapdoor()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(CloseAnimation());
        thisCardsCollider.enabled = true;
    }

    IEnumerator OpenAnimation()
    {
        for(int i =  0; i < 90f/animationSpeed; i++)
        {
            pivotL.transform.eulerAngles -= new Vector3(0, 0, animationSpeed);
            pivotR.transform.eulerAngles -= new Vector3(0, 0, -animationSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CloseAnimation()
    {
        for(int i =  0; i < 90f/animationSpeed; i++)
        {
            pivotL.transform.eulerAngles -= new Vector3(0, 0, animationSpeed);
            pivotR.transform.eulerAngles -= new Vector3(0, 0, -animationSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
