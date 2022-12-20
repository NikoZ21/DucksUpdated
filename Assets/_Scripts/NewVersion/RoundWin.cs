using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.NewVersion
{
    public class RoundWin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem correctVFX;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ProgressBar progressBar;

        public void ProccessRoundWin()
        {
            correctVFX.Play();
            audioSource.clip = audioClip;
            audioSource.Play();
            progressBar.UpdateProgressBar();
            if (progressBar.GetProgressBar() >= 5)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}