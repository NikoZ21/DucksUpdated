using System.Collections;
using UnityEngine;

namespace _Scripts.NewVersion
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            HideHint();
        }

        public void HideHint()
        {
            spriteRenderer.enabled = false;
        }

        public void ShowHint()
        {
            spriteRenderer.enabled = true;
        }
    }
}