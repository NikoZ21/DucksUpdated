using UnityEngine;

namespace _Scripts.NewVersion
{
    public class Bucket : MonoBehaviour, IIdentity
    {
        [SerializeField] private int Id = 1;
        [SerializeField] private ParticleSystem spawnVFX;
        [SerializeField] private SpriteRenderer sprite;

        private void Start()
        {
            EnableSprite(false);
        }

        public void PlayVisualEffect()
        {
            if (spawnVFX == null) return;

            Instantiate(spawnVFX, transform.position, transform.rotation);
            spawnVFX.Play();
        }

        public void EnableSprite(bool state)
        {
            sprite.enabled = state;
        }

        public int GetId()
        {
            return Id;
        }
    }
}