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
    [SerializeField] private Canvas canvas;
    private CanvasGroup _canvasGroup;


    private void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if(!_collider2D.enabled) transform.Rotate(Vector3.forward, scrollInput * 10000 * Time.deltaTime);
        if (_rectTransform.localPosition.x < 150)
        {
            _rectTransform.localScale = new Vector3(1, 1, 1);
           
        }
        
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _collider2D = GetComponent<BoxCollider2D>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _collider2D.enabled = false;
      
    }
    

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
       
    }

   

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Debug.Log("OnDrag");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("onPointerUp");
        _collider2D.enabled = true;
       
    }
    
}
