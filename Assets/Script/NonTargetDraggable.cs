 using UnityEngine;
 using UnityEngine.EventSystems;
 
 public class NonTargetDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    Camera mainCamera;
    float zAxis = 1.0f;
    Transform parentToReturnTo;
     // Use this for initialization
    Quaternion originalAngle;
    Vector3 originalPosition;

    public bool isInDropZone;

    int originalIndex;
    void Start() {
        mainCamera = Camera.main;
    }
 
    public void OnBeginDrag(PointerEventData eventData) {
        originalAngle = this.transform.rotation;
        parentToReturnTo = this.transform.parent;
        originalPosition = this.transform.position;
        originalIndex = this.transform.GetSiblingIndex();
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,0);
        this.transform.SetParent(this.transform.parent.parent);
        zAxis = transform.position.z;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
 
    public void OnDrag(PointerEventData eventData) {
         //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);
        tempVec.z = zAxis; //Make sure that the z zxis never change
        transform.position = tempVec;
    }
 
    public void OnEndDrag(PointerEventData eventData) {
        if (isInDropZone && StageManager.instance.manaCount - eventData.pointerDrag.GetComponent<Cards>().manaCost >= 0) {
            eventData.pointerDrag.GetComponent<Cards>().OnDrop(0);
        } else {
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = originalPosition;
            this.transform.SetSiblingIndex(originalIndex);
            gameObject.GetComponent<RectTransform>().rotation = originalAngle;
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
    }

     //Add Event System to the Camera
    void addEventSystem() {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
}