using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour {

    [SerializeField] private LineRenderer arrowLine;

    [SerializeField] private EdgeCollider2D arrowHead;

    public void Start() {
        arrowHead = this.GetComponent<EdgeCollider2D>();
        arrowLine = this.GetComponent<LineRenderer>(); 
    }

    public void Update() {
        SetEdgeCollider(arrowLine);
    }

    public void SetEdgeCollider(LineRenderer lineRenderer) {
        List<Vector2> edges = new List<Vector2>();
        edges.Add(lineRenderer.GetPosition(0));
        edges.Add(lineRenderer.GetPosition(3));
        arrowHead.SetPoints(edges);
    }
}
