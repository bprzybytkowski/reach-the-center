using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {
    [SerializeField] int segments;
    [SerializeField] float xRadius;
    [SerializeField] float yRadius;
    [SerializeField] float angle;
    [SerializeField] float cutOut;
    [SerializeField] Color color;
    [SerializeField] float rotateSpeed;

    LineRenderer line;
    float lineWidth;
    EdgeCollider2D edgeCollider2D;
    List<Vector2> edgeColliderPoints;
    bool isSpinning = true;

    void Start() {
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

    private void Update() {
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
