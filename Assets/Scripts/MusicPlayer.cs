using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip music;
    private AudioSource source;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = music;
        source.loop = true;
        source.volume = 0.4f;
        source.Play();
    }
}