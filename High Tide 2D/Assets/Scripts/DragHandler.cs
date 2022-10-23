using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DragHandler: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("OnDrag");        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("OnEndDrag");        
    }
}
