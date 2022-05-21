using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    private bool isDragActive = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;
    private Draggable lastDragged;

    void Awake() {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1) {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isDragActive && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) {
            Drop();
            return;
        }
        if (Input.touchCount > 0) {
            screenPosition = Input.GetTouch(0).position;
        } else {
            return;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragActive) {
            Drag();
        } else {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null) {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null) {
                    lastDragged = draggable;
                    initDrag();
                }
            }
        }
    } 

    void initDrag() {
        isDragActive = true;
    }
    void Drag() {
        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }
    void Drop() {
        isDragActive = false;
        //Card needs to activate;
        lastDragged.transform.gameObject.SendMessage("OnDrop");
    }
}

