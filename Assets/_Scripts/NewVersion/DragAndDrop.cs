using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.NewVersion
{
    public class DragAndDrop : MonoBehaviour
    {
        [Header("DuckCallbacks")]
        public UnityEvent OnWronglyMatched;

        public UnityEvent OnCorrectlyMatched;
        public UnityEvent OnMouseDragged;

        
        private Camera _cam;
        private IIdentity _duckIdentity;
        private IPositioned _duckPositioned;
        private bool isMatch = false;

        private void Awake()
        {
            _cam = Camera.main;
            _duckIdentity = GetComponent<IIdentity>();
            _duckPositioned = GetComponent<IPositioned>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var bucketIdentity = collision.GetComponent<IIdentity>();
            if (bucketIdentity == null) return;
            isMatch = bucketIdentity.GetId() == _duckIdentity.GetId();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isMatch = false;
        }

        private void OnMouseUp()
        {
            if (!_duckPositioned.IsPositioned()) return;

            if (isMatch)
            {
                ProccessCorrectMatch();
            }
            else
            {
                ProccessWrongMatch();
            }
        }

        private void ProccessWrongMatch()
        {
            OnWronglyMatched?.Invoke();
        }

        private void ProccessCorrectMatch()
        {
            OnCorrectlyMatched?.Invoke();
        }

        private void OnMouseDrag()
        {
            if (!_duckPositioned.IsPositioned()) return;
            OnMouseDragged?.Invoke();

            transform.position = GetMousePosition();
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }

        Vector3 GetMousePosition()
        {
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}