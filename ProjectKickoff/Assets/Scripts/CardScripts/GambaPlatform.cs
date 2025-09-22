using System.Collections;
using UnityEngine;

public class GambaPlatform : CardBase
{
    public CardBase[] possibleEffects;
    ParticleSystem randomParticles;
    protected override void EnterEffect(Collision2D collision)
    {
        StartCoroutine(RandomizeCard());
    }

    protected override void StartEffect()
    {
        randomParticles = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        if (randomParticles == null) Debug.LogError($"{this.gameObject.name} is missing a particlesystem child");
    }

    IEnumerator RandomizeCard()
    {
        randomParticles.transform.parent = null;
        randomParticles.Play();
        yield return new WaitForSeconds(1);
        SpawnNewPlatform();
        Destroy(randomParticles.gameObject);
    }

    private void SpawnNewPlatform()
    {
        if (possibleEffects.Length < 1)
        {
            Debug.LogWarning("No possible effects set dumbass");
            return;
        }

        int index = Random.Range(0, possibleEffects.Length);
        CardBase spawned = Instantiate(possibleEffects[index], this.transform.position,
            this.transform.rotation, this.transform.parent);
        spawned.originalPrefab = possibleEffects[index];
        GameManager.instance.cardsInDeck.Add(possibleEffects[index]);
        GameManager.instance.cardsInDeck.Remove(originalPrefab);
        Destroy(this.gameObject);
    }
}
