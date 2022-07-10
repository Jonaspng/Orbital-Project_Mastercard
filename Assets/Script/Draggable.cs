using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
 
    Camera mainCamera;

    Vector3 clickOffset = Vector3.zero;

    Vector3 touchWorld;

    Vector3 startPosition;

    Transform parentToReturnTo = null;

    public float arrowHeadSize;

    public GameObject prefabArrow;

    public GameObject arrow;

    public LineRenderer arrowLine;

    public EdgeCollider2D arrowCollider;

    public Quaternion originalRotation;

    public Transform parentToReturn;

    public Vector3 originalPosition;

    public int originalIndex;

    public Enemy[] enemies;
     // Use this for initialization
    void Start() {
        arrowHeadSize = 5.0f;
        touchWorld = new Vector3();
        mainCamera = Camera.main;
        parentToReturn = this.transform.parent;
    }
 
     public void OnBeginDrag(PointerEventData eventData) {
         arrow = Instantiate(prefabArrow, this.transform);
         arrowLine = this.GetComponentInChildren<LineRenderer>();
         parentToReturnTo = this.transform.parent;
         originalPosition = this.transform.position;
         originalRotation = this.transform.rotation;
         originalIndex = this.transform.GetSiblingIndex();
         arrowLine.enabled = true;
         Vector3 newPos = this.transform.position;
         newPos.y += 2;
         this.transform.position = newPos;
         this.transform.rotation = Quaternion.Euler(0,0,0);
         startPosition = newPos;
         this.transform.SetParent(this.transform.parent.parent);
        
     }

    public void DrawArrow(PointerEventData eventData) {
         
        touchWorld = Camera.main.ScreenToWorldPoint(
            new Vector3 (eventData.position.x,
            eventData.position.y,
            Camera.main.nearClipPlane
        ));
        float percentSize = (float) (arrowHeadSize / Vector3.Distance (startPosition, touchWorld));
        arrowLine.SetPosition (0, startPosition);
        arrowLine.SetPosition (1, Vector3.Lerp(startPosition, touchWorld, 0.999f - percentSize));
        arrowLine.SetPosition (2, Vector3.Lerp (startPosition, touchWorld, 1 - percentSize));
        arrowLine.SetPosition (3, touchWorld);
        arrowLine.widthCurve = new AnimationCurve (

        new Keyframe (0, 0.4f),
        new Keyframe (0.999f - percentSize, 0.4f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1 - percentSize, 1f),
        new Keyframe (1, 0f));

        SetCollider();
    }

    public void SetCollider() {
        List<Vector2> edges = new List<Vector2>();
        edges.Add(transform.InverseTransformPoint(arrowLine.GetPosition(2)));
        edges.Add(transform.InverseTransformPoint(arrowLine.GetPosition(3)));
        this.GetComponent<EdgeCollider2D>().SetPoints(edges);
    }
 
    public void OnDrag(PointerEventData eventData) {
        DrawArrow(eventData);
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position);
        GameObject.Destroy(arrow);
        Enemy enemySelected = DetectEnemySelected();
        if (enemySelected != null) {
            Cards temp = this.gameObject.GetComponent<Cards>();
            int number = enemySelected.DamageTaken(GameObject.Find("Player Battlestation").GetComponentInChildren<Player>().GetFullDamage(temp.originalDamage));
            temp.damage = number;
            temp.RefreshString();
        }
     }

    public Enemy DetectEnemySelected() {
        enemies = StageManager.instance.enemies;  
        foreach(Enemy enemy in enemies) {
            if (enemy != null) {
                if (this.GetComponent<EdgeCollider2D>().IsTouching(enemy.GetComponentInChildren<Collider2D>())) {
                    return enemy;
                }
            }
        }
        return null;
    }


     public void OnEndDrag(PointerEventData eventData) {
        Enemy enemySelected = DetectEnemySelected();
        arrowLine.enabled = false;
        if (enemySelected != null && StageManager.instance.manaCount - eventData.pointerDrag.GetComponent<Cards>().manaCost >= 0) {
            this.gameObject.GetComponent<Cards>().OnDrop(enemySelected.enemyIndex);
        } else {
            this.transform.position = originalPosition;
            this.transform.rotation = originalRotation;
            this.transform.SetParent(parentToReturn);
            this.transform.SetSiblingIndex(originalIndex);
        }        
     }
 
     //Add Event System to the Camera
     void addEventSystem() {
         GameObject eventSystem = new GameObject("EventSystem");
         eventSystem.AddComponent<EventSystem>();
         eventSystem.AddComponent<StandaloneInputModule>();
     }
 }