using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// this script has been tested ; sadly it is not working; for reference only
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler {

    public void OnBeginDrag(PointerEventData eventData) {

    }

     public void OnDrag(PointerEventData eventData) {
         this.transform.position = new Vector3(eventData.position.x, eventData.position.y , transform.position.z);
         

    }
    public void OnEndDrag(PointerEventData eventData) {

    }

    
}
