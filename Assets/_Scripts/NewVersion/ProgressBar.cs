using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.NewVersion
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] int progressBar = 0;
        [SerializeField] Image[] points;
        [SerializeField] Sprite fill;

        public int GetProgressBar()
        {
            return progressBar;
        }

        public void UpdateProgressBar()
        {
            progressBar++;
            for (int i = 0; i < progressBar; i++)
            {
                points[i].sprite = fill;
            }
        }
    }
}
