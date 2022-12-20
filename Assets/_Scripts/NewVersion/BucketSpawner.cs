using System.Collections;
using UnityEngine;

namespace _Scripts.NewVersion
{
    public class BucketSpawner : MonoBehaviour
    {
        [SerializeField] ParticleSystem spawnVFX;
        [SerializeField] private Bucket[] buckets;
        [SerializeField] private float spawnDelay;

        void Start()
        {
            foreach (var bucket in buckets)
            {
                StartCoroutine(Spawn(bucket));
            }
        }

        IEnumerator Spawn(Bucket bucket)
        {
            bucket.PlayVisualEffect();
            yield return new WaitForSeconds(spawnDelay);
            bucket.EnableSprite(true);
        }
    }
}