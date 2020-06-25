using UnityEngine;
using System.Collections;

public class CameraColorShift : MonoBehaviour {

    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    private new Camera camera;

    private void Awake() {
        camera = GetComponent<Camera>();
        StartCoroutine(ShiftOverTime(GameObject.FindObjectOfType<Counter>().getSeconds()));
    }

    IEnumerator ShiftOverTime(float time) {
        float currentTime = 0.0f;

        do {
            camera.backgroundColor = Color.Lerp(startColor, endColor, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}