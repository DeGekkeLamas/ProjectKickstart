using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    AudioSource source;
    public static AudioPlayer instance;
    [Header("Audioclips")]
    public AudioClip placementSound;
    void Awake()
    {
        source = this.GetComponent<AudioSource>();
        instance = this;
    }

    public static void Play(AudioClip clip)
    {
        instance.source.PlayOneShot(clip);
    }
}
