using UnityEngine;
using UnityEngine.EventSystems;

 
public class DropZone : MonoBehaviour, IDropHandler {

     public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag.GetComponent<LineRenderer>() == null) {
            eventData.pointerDrag.GetComponent<NonTargetDraggable>().isInDropZone = true;
            // eventData.pointerDrag.GetComponent<Cards>().OnDrop(0);
        }
         
    }



     
}