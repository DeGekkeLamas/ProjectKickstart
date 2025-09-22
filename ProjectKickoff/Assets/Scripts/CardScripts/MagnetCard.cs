using System.Collections;
using UnityEngine;

public class MagnetCard : CardBase
{
    public float gravity;
    public float effectRadiuS;
    public bool magnetic;
    private Coroutine coroutine;

    protected override void UpdateEffect()
    {
        if (!magnetic) return;
        PlayerController player = PlayerController.instance;
        if (player == null) return;

        Vector2 playerPos = player.transform.position;
        Vector2 platformPos = transform.position;
        Vector2 diff = platformPos - playerPos;
        float distance = diff.magnitude;

        if (distance > effectRadiuS) return;

        Vector2 attraction = diff.normalized / (distance * distance) * gravity;
        float attractionY = attraction.y;
        float gravityY = Mathf.Abs(Physics2D.gravity.y);

        if (attractionY < gravityY) attraction.y = 0;

        Vector2 moveDelta = attraction * Time.deltaTime;
        player.DoMove(moveDelta);
    }

    protected override void EnterEffect(Collision2D collision)
    {
        magnetic = false;
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    protected override void ExitEffect(Collision2D collision)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Remagnetize());
        }
        
    }

    private IEnumerator Remagnetize()
    {
        yield return new WaitForSeconds(0.5f);
        magnetic = true;
    }
}
