using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace _Scripts.NewVersion
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Duck[] ducks;
        [SerializeField] private Transform leftSpawner;
        [SerializeField] private Transform rightSpawner;
        [SerializeField] private Transform parent;
        [SerializeField] private int totalDucks;
        [SerializeField] private List<Vector3> stopPositions;
        [SerializeField] private List<GameObject> ducksSpawned;
        public UnityEvent onRoundWin;
        private HintManager _hintManager;

        private void Awake()
        {
            _hintManager = GetComponent<HintManager>();
        }

        private void Start()
        {
            ducks = Resources.LoadAll<Duck>("");

            SetStopPositions();


            foreach (var duck in ducks)
            {
                if (duck.isLefty)
                {
                    StartCoroutine(SetUpDuck(duck, rightSpawner));
                }
                else
                {
                    StartCoroutine(SetUpDuck(duck, leftSpawner));
                }
            }
        }

        private IEnumerator SetUpDuck(Duck duck, Transform spawner)
        {
            var newDuck = Instantiate(duck.gameObject, spawner.position, quaternion.identity, spawner);
            newDuck.GetComponent<DragAndDrop>().enabled = false;
            newDuck.GetComponent<Duck>().onPositionedCorrectly.AddListener(IncreaseDucks);
            ducksSpawned.Add(newDuck);
            _hintManager.Hints.Add(newDuck.GetComponent<Hint>());
            RandomizePosition(newDuck.GetComponent<Duck>());
            yield return new WaitForSeconds(0.5f);
            newDuck.GetComponent<DragAndDrop>().OnCorrectlyMatched.AddListener(DeacresDucks);
        }

        private void RandomizePosition(Duck duck)
        {
            if (stopPositions.Count == 1)
            {
                duck.SetStopPosition(stopPositions[0]);
                stopPositions.RemoveAt(0);
                return;
            }

            var index = Random.Range(0, stopPositions.Count);
            duck.SetStopPosition(stopPositions[index]);
            stopPositions.RemoveAt(index);
        }

        private void SetStopPositions()
        {
            var lineStart = new Vector3(-6, -2, 0);
            var lineLength = 12;
            var pointDistance = lineLength / (ducks.Length - 1);
            for (int i = 0; i < ducks.Length; i++)
            {
                float x = lineStart.x + (i * pointDistance);
                stopPositions.Add(new Vector3(x, -2, 0));
            }
        }

        private void IncreaseDucks()
        {
            totalDucks++;
            if (totalDucks == ducks.Length)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            foreach (var duck in ducks)
            {
                duck.GetComponent<DragAndDrop>().enabled = true;
            }
        }

        private void DeacresDucks()
        {
            totalDucks--;
            if (totalDucks <= 0)
            {
                onRoundWin?.Invoke();
                foreach (var duck in ducksSpawned)
                {
                    duck.GetComponent<Duck>().ResetDucks();
                }
            }
        }
    }
}