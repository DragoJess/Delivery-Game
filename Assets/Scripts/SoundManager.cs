using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource soundObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        soundObject.volume = volume;
        soundObject.PlayOneShot(audioClip);
    }

    public IEnumerator PlayRepeatingSound(AudioClip audioClip, Transform spawnTransform, float volume, float delay)
    {
        while (true)
        {
            PlaySound(audioClip, spawnTransform, volume);
            yield return new WaitForSeconds(delay);
        }
    }
}
