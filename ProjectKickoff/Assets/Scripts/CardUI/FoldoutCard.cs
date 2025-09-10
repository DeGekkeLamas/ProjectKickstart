using System.Collections;
using UnityEngine;

public class FoldoutCard : MonoBehaviour
{
    Vector3 originalScale;
    Vector3 originalRotation;
    public float maxScaleFactor = 3;
    public int animationDuration = 30;
    public float extraHeight = 30;

    bool isFoldingOut;
    bool isFoldingIn;
    bool hasEnteredHover;
    private void Awake()
    {
        originalScale = transform.localScale;
    }
    /// <summary>
    /// Folds the card from your hand out to be better visible
    /// </summary>
    public void Foldout()
    {
        if (!isFoldingOut && !isFoldingIn && !hasEnteredHover)
        {
            hasEnteredHover = true;
            StartCoroutine(FoldoutAnimation());
            Debug.Log("Folded out card");
        }
    }
    IEnumerator FoldoutAnimation()
    {
        // Wait if animation is still ongoing
        while (isFoldingIn)
        {
            yield return null;
        }

        isFoldingOut = true;
        originalRotation = this.transform.localEulerAngles;
        float inverseDuration = 1f / animationDuration;
        float factor = Mathf.Pow(maxScaleFactor, inverseDuration);

        // Movements
        for (int i = 0; i < animationDuration; i++)
        {
            this.transform.localScale *= factor;
            this.transform.eulerAngles = Vector3.Lerp(originalRotation, !CloserToPlusSide(originalRotation.z, 0) ? 
                Vector3.zero : new(0,0,360), inverseDuration * (i+1));
            this.transform.position += new Vector3(0,extraHeight/animationDuration,0) * transform.root.localScale.x;

            yield return new WaitForFixedUpdate();
        }
        isFoldingOut = false;
    }
    /// <summary>
    /// Folds card back into your hand
    /// </summary>
    public void Foldin()
    {
        if (!isFoldingIn && hasEnteredHover)
        {
            StartCoroutine(FoldinAnimation());
            Debug.Log("Folded in card");
        }
    }
    IEnumerator FoldinAnimation()
    {
        // Wait if animation is still ongoing
        isFoldingIn = true;
        while (isFoldingOut)
        {
            yield return null;
        }

        float inverseDuration = 1f / animationDuration;
        float factor = Mathf.Pow(1f/maxScaleFactor, inverseDuration);

        // Movements
        for (int i = 0; i < animationDuration; i++)
        {
            this.transform.localScale *= factor;
            this.transform.eulerAngles = Vector3.Lerp(!CloserToPlusSide(originalRotation.z, 0) ?
                Vector3.zero : new(0, 0, 360), originalRotation, inverseDuration * (i+1));
            this.transform.position -= new Vector3(0, extraHeight/animationDuration, 0) * transform.root.localScale.x;

            yield return new WaitForFixedUpdate();
        }
        isFoldingIn = false;
        hasEnteredHover = false;
    }

    /// <summary>
    /// Determines which direction is shorter for rotation
    /// </summary>
    static bool CloserToPlusSide(float currentRotation, float destination)
    {
        float differenceUp = (360 + destination - currentRotation) % 360;
        float differenceDown = (360 + currentRotation - destination) % 360;
        return differenceDown > differenceUp;
    }
}
