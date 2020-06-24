using UnityEngine;
using System.Collections;

public class CameraColorShift : MonoBehaviour {

    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    private float t = 0;
    private new Camera camera;
    Color lerpedColor;

    private void Awake() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        lerpedColor = Color.Lerp(startColor, endColor, t);
        if (t < 1) {
            t += Time.deltaTime / GameObject.FindObjectOfType<Counter>().getSeconds();
        }
        camera.backgroundColor = lerpedColor;
    }
}