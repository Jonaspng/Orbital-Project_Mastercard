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

    public Enemy[] enemies;
     // Use this for initialization
    void Start() {
                   
        arrowHeadSize = 5.0f;
        touchWorld = new Vector3();
        mainCamera = Camera.main;

    }
 
     public void OnBeginDrag(PointerEventData eventData) {
        //  zAxis = transform.position.z;
        //  clickOffset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, zAxis)) + new Vector3(0, 3, 0);
        //  transform.position = new Vector3(transform.position.x, transform.position.y, zAxis);
         arrow = Instantiate(prefabArrow, this.transform);
         arrowLine = this.GetComponentInChildren<LineRenderer>();
         parentToReturnTo = this.transform.parent;
         arrowLine.enabled = true;
         startPosition = this.transform.position;
        
     }

    public void DrawArrow(PointerEventData eventData) {
         
        touchWorld = Camera.main.ScreenToWorldPoint(
            new Vector3 (eventData.position.x,
            eventData.position.y,
            Camera.main.nearClipPlane
        ));
        //The longer the line gets, the smaller relative to the entire line the arrowhead should be
        float percentSize = (float) (arrowHeadSize / Vector3.Distance (startPosition, touchWorld));
        //h/t ShawnFeatherly (http://answers.unity.com/answers/1330338/view.html)
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

     }

    public Enemy DetectEnemySelected() {
        enemies = StageManager.instance.enemies;  
        foreach(Enemy enemy in enemies) {
            if (this.GetComponent<EdgeCollider2D>().IsTouching(enemy.GetComponentInChildren<Collider2D>())) {
                return enemy;
            }
        }
        return null;
    }

 
 
     public void OnEndDrag(PointerEventData eventData) {
        Enemy enemySelected = DetectEnemySelected();
        if (enemySelected != null) {
            this.gameObject.GetComponent<BasicAttack>().Testing(enemySelected.enemyIndex);
        }
        arrowLine.enabled = false;

     }
 
     //Add Event System to the Camera
     void addEventSystem() {
         GameObject eventSystem = new GameObject("EventSystem");
         eventSystem.AddComponent<EventSystem>();
         eventSystem.AddComponent<StandaloneInputModule>();
     }
 }