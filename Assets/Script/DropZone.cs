using UnityEngine;
using UnityEngine.EventSystems;

 
public class DropZone : MonoBehaviour, IDropHandler {
     public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.GetComponent<LineRenderer>() == null) {
            eventData.pointerDrag.GetComponent<NonTargetDraggable>().SetInDropZone(true);
        }
         
    }
}