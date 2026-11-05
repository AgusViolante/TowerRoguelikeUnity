using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }

    [SerializeField] AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;
    }

    public static void PlaySfx(AudioClip clip, float volume = 1f)
    {
        if (Instance == null || clip == null) return;
        Instance.sfxSource.PlayOneShot(clip,volume);
    }
}
