using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {
    [SerializeField] int segments;
    [SerializeField] float angle;
    [SerializeField] float cutOut;
    [SerializeField] Color color;
    [SerializeField] float rotateSpeed;
    [SerializeField] Radius radius;
    float xRadius;
    float yRadius;
    LineRenderer line;
    float lineWidth;
    EdgeCollider2D edgeCollider2D;
    List<Vector2> edgeColliderPoints;
    bool isSpinning = true;

    void Start() {
        switch (radius) {
            case Radius.Tiny:
                xRadius = 0.26f;
                yRadius = 0.26f;
                break;
            case Radius.Small:
                xRadius = 0.58f;
                yRadius = 0.58f;
                break;
            case Radius.Medium:
                xRadius = 0.9f;
                yRadius = 0.9f;
                break;
            case Radius.Large:
                xRadius = 1.22f;
                yRadius = 1.22f;
                break;
            case Radius.Huge:
                xRadius = 1.54f;
                yRadius = 1.54f;
                break;
        }

        line = gameObject.GetComponent<LineRenderer>();
        lineWidth = line.endWidth;
        edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        edgeColliderPoints = new List<Vector2>();

        line.startColor = color;
        line.endColor = color;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
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