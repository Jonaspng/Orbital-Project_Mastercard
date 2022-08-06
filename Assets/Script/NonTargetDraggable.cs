 using UnityEngine;
 using UnityEngine.EventSystems;
 
 public class NonTargetDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zAxis = 1.0f;
    [SerializeField] private Transform parentToReturnTo;
    [SerializeField] private Quaternion originalAngle;
    [SerializeField] private Vector3 originalPosition;

    [SerializeField] private bool isInDropZone;

    [SerializeField] private int originalIndex;
    private void Start() {
        mainCamera = Camera.main;
    }

    public void SetInDropZone(bool boolean) {
        this.isInDropZone = boolean;
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
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);
        tempVec.z = zAxis;
        transform.position = tempVec;
    }
 
    public void OnEndDrag(PointerEventData eventData) {
        if (isInDropZone && StageManager.GetInstance().GetManaCount() - eventData.pointerDrag.GetComponent<Cards>().GetManaCost() >= 0) {
            eventData.pointerDrag.GetComponent<Cards>().OnDrop(0);
        } else {
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = originalPosition;
            this.transform.SetSiblingIndex(originalIndex);
            gameObject.GetComponent<RectTransform>().rotation = originalAngle;
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
    }
}