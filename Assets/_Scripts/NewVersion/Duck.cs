using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.NewVersion
{
    public class Duck : MonoBehaviour, IIdentity, IPositioned
    {
        [Header("Duck Identity")]
        [SerializeField] public bool isLefty = false;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int Id = 1;


        [Header("Duck Movement")]
        [SerializeField] public Vector3 stopPosition;

        [SerializeField] private bool isPositioned;
        [SerializeField] private float speed = 10;
        public UnityEvent onPositionedCorrectly;
        private DragAndDrop _dragAndDrop;
        private Vector3 startPosition;

        private void Start()
        {
            _dragAndDrop = GetComponent<DragAndDrop>();
            startPosition = transform.position;
            _dragAndDrop.OnCorrectlyMatched.AddListener(DisableDuck);
            _dragAndDrop.OnWronglyMatched.AddListener(ResetPositions);
        }

        private void Update()
        {
            if (isPositioned) return;

            transform.position = Vector3.MoveTowards(transform.position, stopPosition, speed * Time.deltaTime);
            if (transform.position == stopPosition)
            {
                isPositioned = true;
                onPositionedCorrectly?.Invoke();
            }
        }

        public void SetStopPosition(Vector3 position)
        {
            stopPosition = position;
        }

        public bool IsPositioned()
        {
            return isPositioned;
        }

        public int GetId()
        {
            return Id;
        }

        public void DisableDuck()
        {
            spriteRenderer.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Hint>().enabled = false;
        }

        public void ResetPositions()
        {
            transform.position = stopPosition;
            transform.localScale = new Vector3(1, 1, 1);
        }


        public void ResetDucks()
        {
            transform.position = startPosition;
            transform.localScale = new Vector3(1, 1, 1);
            spriteRenderer.enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<Hint>().enabled = true;
            isPositioned = false;
        }
    }
}