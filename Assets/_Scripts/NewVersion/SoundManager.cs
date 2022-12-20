using UnityEngine;

namespace _Scripts.NewVersion
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Stop Sound")] [SerializeField]
        AudioClip stopSFX;

        [SerializeField] [Range(0, 1f)] float stopSFXVolume = 0.5f;

        [Header("Correct Sound")] [SerializeField]
        AudioClip correctSFX;

        [SerializeField] [Range(0, 1f)] float correctSFXVolume = 0.5f;

        [Header("Wrong Sound")] [SerializeField]
        AudioClip wrongSFX;

        [SerializeField] [Range(0, 1f)] float wrongSFXVolume = 0.5f;

        [Header("Pop Sound")] [SerializeField] AudioClip popSFX;
        [SerializeField] [Range(0, 1f)] float popSFXVolume = 0.5f;

        [SerializeField] private AudioSource audioSource;


        public void PlayStopSound()
        {
            audioSource.clip = stopSFX;
            audioSource.volume = stopSFXVolume;
            audioSource.Play();
        }

        public void PlayCorrectSound()
        {
            audioSource.clip = correctSFX;
            audioSource.volume = correctSFXVolume;
            audioSource.Play();
        }

        public void PlayWrongSound()
        {
            audioSource.clip = wrongSFX;
            audioSource.volume = wrongSFXVolume;
            audioSource.Play();
        }

        public void PlayPopSound()
        {
            audioSource.clip = popSFX;
            audioSource.volume = popSFXVolume;
            audioSource.Play();
        }
    }
}