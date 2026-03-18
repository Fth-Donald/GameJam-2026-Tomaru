using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    public AudioClip clip;

    public AudioSource source;

    [Range(0f, 1f)]
    public float volume = 1f;

    void Awake()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        if (source != null && clip != null)
        {
            source.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("[PlaySfx] lose AudioSource or AudioClip");
        }
    }
}