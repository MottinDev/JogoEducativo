using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private bool _isDragActive = false;
    [SerializeField] private Vector2 _screenPosition;
    [SerializeField] private Vector3 _worldPosition;
    [SerializeField] private Draggable _lastDragged;

    void Awake()
    {
        Debug.Log("DragController iniciado");
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (_isDragActive)
        {
            if(Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Debug.Log("drop");
                Drop();
                return;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("click do mouse");
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            Debug.Log("sem evento");
            return;
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (_isDragActive)
        {
            Debug.Log("arrastando");
            Drag();
        }
        else
        {
            Debug.Log("preparar para arrastar");
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();

                if (!draggable.IsAtivado()) return;

                if (draggable != null)
                {
                    Debug.Log("começar a arrastar");
                    _lastDragged = draggable;
                    InitDrag();
                }
            }
        }

        void InitDrag()
        {
            Debug.Log("_isDragActive = true");
            _isDragActive = true;
        }

        void Drag()
        {
            Debug.Log("arrasto para nova posição");
            _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
        }

        void Drop()
        {
            Debug.Log("_isDragActive = false");
            _isDragActive = false;
        }
    }
}