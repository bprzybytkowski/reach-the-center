using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {
    [SerializeField] int segments = 50;
    [SerializeField] float angle = 0;
    [SerializeField] float cutOut = 0;
    [SerializeField] Color color;
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] Radius radius;
    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] bool isEnabled;
    float xRadius;
    float yRadius;
    LineRenderer line;
    EdgeCollider2D edgeCollider2D;
    List<Vector2> edgeColliderPoints;
    bool isSpinning = true;

    void Start() {
        if (!isEnabled) {
            gameObject.SetActive(false);
        }

        line = gameObject.GetComponent<LineRenderer>();
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        edgeColliderPoints = new List<Vector2>();

        line.startColor = color;
        line.endColor = color;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;

        switch (radius) {
            case Radius.Tiny:
                xRadius = 0.11f + (line.startWidth / 2);
                yRadius = 0.11f + (line.startWidth / 2);
                break;
            case Radius.Small:
                xRadius = 0.43f + (line.startWidth / 2);
                yRadius = 0.43f + (line.startWidth / 2);
                break;
            case Radius.Medium:
                xRadius = 0.75f + (line.startWidth / 2);
                yRadius = 0.75f + (line.startWidth / 2);
                break;
            case Radius.Large:
                xRadius = 1.07f + (line.startWidth / 2);
                yRadius = 1.07f + (line.startWidth / 2);
                break;
            case Radius.Huge:
                xRadius = 1.39f + (line.startWidth / 2);
                yRadius = 1.39f + (line.startWidth / 2);
                break;
        }

        CreatePoints();
    }

    private void FixedUpdate() {
        if (isSpinning) {
            Rotate();
        }
    }

    void CreatePoints() {
        float lineX;
        float lineY;
        float colliderX;
        float colliderY;
        float z = 0f;

        for (int i = 0; i < (segments + 1); i++) {

            lineX = Mathf.Sin(Mathf.Deg2Rad * angle) * xRadius;
            lineY = Mathf.Cos(Mathf.Deg2Rad * angle) * yRadius;

            colliderX = Mathf.Sin(Mathf.Deg2Rad * angle) * (xRadius + lineWidth / 2);
            colliderY = Mathf.Cos(Mathf.Deg2Rad * angle) * (xRadius + lineWidth / 2);

            Vector3 pointPosition = new Vector3(lineX, lineY, z);
            line.SetPosition(i, pointPosition);

            Vector3 colliderPosition = new Vector3(colliderX, colliderY, z);
            edgeColliderPoints.Add(colliderPosition);

            angle += ((360f - cutOut) / segments);
        }

        edgeCollider2D.points = edgeColliderPoints.ToArray();
    }

    void Rotate() {
        transform.RotateAround(Vector3.zero, Vector3.forward, Time.fixedDeltaTime * rotateSpeed);
    }

    public void StopSpinning() {
        isSpinning = false;
    }
}

public enum Radius {
    Tiny, Small, Medium, Large, Huge
}