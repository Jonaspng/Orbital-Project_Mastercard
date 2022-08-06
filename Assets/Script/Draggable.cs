using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(EdgeCollider2D))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
 
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Vector3 clickOffset = Vector3.zero;

    [SerializeField] private Vector3 touchWorld;

    [SerializeField] private Vector3 startPosition;

    [SerializeField] private Transform parentToReturnTo = null;

    [SerializeField] private float arrowHeadSize;

    [SerializeField] private GameObject prefabArrow;

    [SerializeField] private GameObject arrow;

    [SerializeField] private LineRenderer arrowLine;

    [SerializeField] private EdgeCollider2D arrowCollider;

    [SerializeField] private Quaternion originalRotation;

    [SerializeField] private Transform parentToReturn;

    [SerializeField] private Vector3 originalPosition;

    [SerializeField] private int originalIndex;

    public Enemy[] enemies;

    private void Start() {
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
            int number;
            if (enemySelected.CheckBurn() && temp is Fireball) {
                number = enemySelected.FireballDamageTaken(GameObject.Find("Player Battlestation").GetComponentInChildren<Player>().GetFullDamage(temp.GetOriginalDamage()));
            } else {
                number = enemySelected.DamageTaken(GameObject.Find("Player Battlestation").GetComponentInChildren<Player>().GetFullDamage(temp.GetOriginalDamage()));
            }
            temp.SetDamage(number);
            temp.RefreshString();
        }
     }

    public Enemy DetectEnemySelected() {
        enemies = StageManager.GetInstance().GetEnemies();  
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
        if (enemySelected != null && StageManager.GetInstance().GetManaCount() - eventData.pointerDrag.GetComponent<Cards>().GetManaCost() >= 0) {
            this.gameObject.GetComponent<Cards>().OnDrop(enemySelected.GetEnemyIndex());
        } else {
            this.transform.position = originalPosition;
            this.transform.rotation = originalRotation;
            this.transform.SetParent(parentToReturn);
            this.transform.SetSiblingIndex(originalIndex);
        }        
    }
 
    void addEventSystem() {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
 }