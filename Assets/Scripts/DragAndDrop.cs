using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,IEndDragHandler, IDragHandler, IPointerUpHandler
{


    private RectTransform _rectTransform;
    private BoxCollider2D _collider2D;
    private float scrollInput;
    private bool SelectedPiece;


    private void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if(SelectedPiece) transform.Rotate(Vector3.forward, scrollInput * 1000 * Time.deltaTime);
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
        _collider2D.enabled = false;
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("onEndDrag");
    }

   

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
        Debug.Log("OnDrag");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("onPointerUp");
        _collider2D.enabled = true;
    }
    
}
