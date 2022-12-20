using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.NewVersion
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private List<Bucket> buckets;
        [SerializeField] private float delayBetweenHints = 0.7f;
        [SerializeField] private float inactiveTimer = 8;
        public List<Hint> Hints;
        private Hint currentBucketHint;
        private Hint currentDuckHint;
        private bool shouldHint = true;
        private Coroutine _hintCoroutine;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            foreach (var hint in Hints)
            {
                hint.GetComponent<DragAndDrop>().OnMouseDragged.AddListener(ChangeToActiveState);
            }
        }

        private void ChangeToActiveState()
        {
            inactiveTimer = 8;
            shouldHint = true;
            if (_hintCoroutine == null) return;
            currentBucketHint.HideHint();
            currentDuckHint.HideHint();
            StopCoroutine(_hintCoroutine);
        }


        private void Update()
        {
            inactiveTimer -= Time.deltaTime;
            if (inactiveTimer <= 0 && shouldHint)
            {
                _hintCoroutine = StartCoroutine(ShowHints());
                shouldHint = false;
            }
        }

        private IEnumerator ShowHints()
        {
            CheckIfCompleted();
            currentDuckHint.ShowHint();
            yield return new WaitForSeconds(delayBetweenHints);
            currentDuckHint.HideHint();
            foreach (var bucket in buckets)
            {
                if (bucket.GetId() != currentDuckHint.GetComponent<IIdentity>().GetId()) continue;
                currentBucketHint = bucket.GetComponent<Hint>();
                break;
            }

            currentBucketHint.ShowHint();

            yield return new WaitForSeconds(delayBetweenHints);

            currentBucketHint.HideHint();
            inactiveTimer = 8;
            shouldHint = true;
        }

        private void CheckIfCompleted()
        {
            var index = Random.Range(0, Hints.Count);

            if (Hints[index].isActiveAndEnabled == false)
            {
                CheckIfCompleted();
            }
            else
            {
                currentDuckHint = Hints[index];
            }
        }
    }
}