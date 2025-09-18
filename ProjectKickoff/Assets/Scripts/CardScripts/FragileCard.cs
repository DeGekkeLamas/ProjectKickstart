using System.Collections;
using UnityEngine;

public class FragileCard : CardBase
{
    ParticleSystem breakingParticles;

    private void Start()
    {
        breakingParticles = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        if (breakingParticles == null) Debug.LogError($"{this.gameObject.name} is missing a particlesystem child");
    }
    protected override void ExitEffect(Collision2D collision)
    {
        StartCoroutine(BreakCard());
    }

    /// <summary>
    /// For if we want to animate it later
    /// </summary>
    IEnumerator BreakCard()
    {
        breakingParticles.transform.parent = null;
        breakingParticles.Play();
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1);
        Destroy(breakingParticles.gameObject);
    }
}
