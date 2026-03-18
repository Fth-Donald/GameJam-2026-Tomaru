using UnityEngine;
using System.Collections;

public class SfxPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;

    void Awake()
    {
        if (source == null) source = GetComponent<AudioSource>();
    }

    public void Play(int index, float volume = 1f)
    {
        if (!IsValid(index)) return;
        source.PlayOneShot(clips[index], Mathf.Clamp01(volume));
    }

    public void PlayDelayed(int index, float delay, float volume = 1f)
    {
        if (!IsValid(index)) return;
        StartCoroutine(IEPlayDelayed(index, delay, volume));
    }

    public void PlayRandom(int fromInclusive, int toExclusive, float volume = 1f)
    {
        int idx = Random.Range(fromInclusive, toExclusive);
        Play(idx, volume);
    }

    private IEnumerator IEPlayDelayed(int index, float delay, float volume)
    {
        yield return new WaitForSeconds(delay);
        Play(index, volume);
    }

    private bool IsValid(int index)
    {
        if (source == null) { Debug.LogWarning("[SfxPlayer] AudioSource null"); return false; }
        if (clips == null || index < 0 || index >= clips.Length) { Debug.LogWarning($"[SfxPlayer] error : {index}"); return false; }
        if (clips[index] == null) { Debug.LogWarning($"[SfxPlayer] clips[{index}] null"); return false; }
        return true;
    }
}