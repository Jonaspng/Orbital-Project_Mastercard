 using UnityEngine;
 using UnityEngine.EventSystems;

 
 public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler{

     public void OnPointerEnter(PointerEventData eventData) {
     }

     public void OnDrop(PointerEventData eventData) {
         if (eventData.pointerDrag.GetComponent<LineRenderer>() == null) {
             eventData.pointerDrag.GetComponent<Cards>().OnDrop(0);
         }
         
     }

     
 }