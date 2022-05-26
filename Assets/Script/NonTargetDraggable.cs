 using UnityEngine;
 using UnityEngine.EventSystems;
 
 public class NonTargetDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
     Camera mainCamera;
     float zAxis = 0;
     Vector3 clickOffset = Vector3.zero;

     Transform parentToReturnTo;
     // Use this for initialization
     void Start() {
         parentToReturnTo = this.transform.parent;
         mainCamera = Camera.main;
         if (mainCamera.GetComponent<Physics2DRaycaster>() == null)
             mainCamera.gameObject.AddComponent<Physics2DRaycaster>();
     }
 
     public void OnBeginDrag(PointerEventData eventData) {
         this.transform.SetParent(this.transform.parent.parent);
         zAxis = transform.position.z;
         transform.position = new Vector3(transform.position.x, transform.position.y, zAxis);
     }
 
     public void OnDrag(PointerEventData eventData) {
         //Use Offset To Prevent Sprite from Jumping to where the finger is
         Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);
         tempVec.z = zAxis; //Make sure that the z zxis never change
         
 
         transform.position = tempVec;
     }
 
     public void OnEndDrag(PointerEventData eventData) {
        this.GetComponent<Cards>().OnDrop(0);
        if (StageManager.instance.manaCount - this.GetComponent<Cards>().manaCost <= 0) {
            this.transform.SetParent(parentToReturnTo);
        }
     }
 
     //Add Event System to the Camera
     void addEventSystem() {
         GameObject eventSystem = new GameObject("EventSystem");
         eventSystem.AddComponent<EventSystem>();
         eventSystem.AddComponent<StandaloneInputModule>();
     }
 }